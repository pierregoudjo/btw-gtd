﻿using System;
using System.Collections.Generic;
using Gtd.Shell.Filters;
using System.Linq;

namespace Gtd.Client.Models
{
    

    public sealed class FilterService
    {
        public readonly List<IFilterCriteria> Filters = new List<IFilterCriteria>(); 
        public FilterService()
        {
            Filters.AddRange(FilterCriteria.LoadAllFilters());

        }
    }


    public interface IItemModel
    {
        string GetTitle();
    }


    public sealed class MutableProject : IItemModel
    {
        public ProjectId ProjectId { get; private set; }
        public string Outcome { get; private set; }
        public ProjectType Type { get; private set; }
        public readonly string UIKey;

        public MutableProject(ProjectId projectId, string outcome, ProjectType type)
        {
            ProjectId = projectId;
            Outcome = outcome;
            Type = type;

            UIKey = "project-" + projectId.Id;  
        }


        public List<MutableAction> Actions = new List<MutableAction>();

        public void OutcomeChanged(string outcome)
        {
            Outcome = outcome;
        }

        public void TypeChanged(ProjectType type)
        {
            Type = type;
        }

        public string GetTitle()
        {
            return string.Format("Project '{0}'", Outcome);
        }
    }

    public sealed class MutableAction : IItemModel
    {
        public ActionId Id { get; private set; }
        public string Outcome { get; private set; }
        public bool Completed { get; private set; }
        public bool Archived { get; private set; }
        public ProjectId ProjectId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime DueDate { get; private set; }

        public string UIKey { get { return "action-" + Id.Id; } }

        public MutableAction(ActionId action, string outcome, ProjectId project)
        {
            Id = action;
            Outcome = outcome;
            Completed = false;
            Archived = false;
            ProjectId = project;
        }

        public void MarkAsCompleted()
        {
            Completed = true;
        }
        public void OutcomeChanged(string outcome)
        {
            Outcome = outcome;
        }

        public string GetTitle()
        {
            return string.Format("Action: '{0}'", Outcome);
        }

        public void MarkAsArchived()
        {
            Archived = true;

        }

        public void StartDateAssigned(DateTime newStartDate)
        {
            StartDate = newStartDate;
        }
        public void DueDateAssigned(DateTime newDueDate)
        {
            DueDate = newDueDate;
        }
    }

    
    public sealed class ClientModel
    {
        readonly IMessageQueue _queue;
        readonly List<MutableThought> _thoughts = new List<MutableThought>();
        public List<MutableProject> ProjectList = new List<MutableProject>();
        public Dictionary<ProjectId, MutableProject> ProjectDict = new Dictionary<ProjectId, MutableProject>();
        public Dictionary<ActionId, MutableAction> ActionDict = new Dictionary<ActionId, MutableAction>();
        public Dictionary<Guid, IItemModel> DictOfAllItems = new Dictionary<Guid, IItemModel>();


        public ImmutableInbox GetInbox()
        {
            var thoughts = _thoughts.Select(t => new ImmutableThought(t.Id, t.Subject, t.UIKey)).ToList().AsReadOnly();
            return new ImmutableInbox(thoughts);
        }
        public int GetNumberOfThoughtsInInbox()
        {
            return _thoughts.Count;
        }

        public readonly TrustedSystemId Id;

        public ClientModel(IMessageQueue queue, TrustedSystemId id)
        {
            _queue = queue;
            Id = id;
        }

        bool _loadingCompleted;

        public void LoadingCompleted()
        {
            _loadingCompleted = true;

            Publish(new Dumb.ClientModelLoaded());
        }

        void Publish(Dumb.CliendModelEvent e)
        {
            if (!_loadingCompleted)
                return;
            _queue.Enqueue(e);
        }

        public void ThoughtCaptured(ThoughtId thoughtId, string thought, DateTime date)
        {
            var item = new MutableThought(thoughtId, thought, date);
            _thoughts.Add(item);
            DictOfAllItems.Add(thoughtId.Id, item);
            Publish(new Dumb.ThoughtAdded(item.Id, item.Subject, item.UIKey));
        }

        public void ThoughtArchived(ThoughtId thoughtId)
        {

             var thought = this._thoughts.SingleOrDefault(t => t.Id == thoughtId);
            if (null == thought)
                return;

            _thoughts.Remove(thought);

            Publish(new Dumb.ThoughtRemoved(thoughtId,thought.UIKey));
        }


        public void ProjectDefined(ProjectId projectId, string projectOutcome, ProjectType type)
        {
            var project = new MutableProject(projectId, projectOutcome, type);
            ProjectList.Add(project);
            ProjectDict.Add(projectId, project);
            DictOfAllItems.Add(projectId.Id, project);

            Publish(new Dumb.ProjectAdded(project.UIKey, projectOutcome, projectId));
        }

        public void ActionDefined(ProjectId projectId, ActionId actionId, string outcome)
        {
            var action = new MutableAction(actionId, outcome, projectId);

            var project = ProjectDict[projectId];
            project.Actions.Add(action);
            ActionDict.Add(actionId, action);
            DictOfAllItems.Add(actionId.Id, action);

            Publish(new Dumb.ActionAdded(actionId, action.UIKey, projectId, project.UIKey, outcome));
        }
        public void ActionCompleted(ActionId actionId)
        {
            var action = ActionDict[actionId];
            var project = ProjectDict[action.ProjectId];
            action.MarkAsCompleted();
            Publish(new Dumb.ActionUpdated(actionId, action.UIKey, action.ProjectId, project.UIKey, action.Outcome, true));
        }

        public void ThoughtSubjectChanged(ThoughtId thoughtId, string subject)
        {
            ((MutableThought)DictOfAllItems[thoughtId.Id]).UpdateSubject(subject);
        }
        public void ProjectOutcomeChanged(ProjectId projectId, string outcome)
        {
            ProjectDict[projectId].OutcomeChanged(outcome);
        }
        public void ActionOutcomeChanged(ActionId actionId, string outcome)
        {
            ActionDict[actionId].OutcomeChanged(outcome);
        }

        public void ActionArchived(ActionId id)
        {
            ActionDict[id].MarkAsArchived();
        }

        public void ProjectTypeChanged(ProjectId projectId, ProjectType type)
        {
            ProjectDict[projectId].TypeChanged(type);
        }

        public void DeferredUtil(ActionId actionId, DateTime newStartDate)
        {
            ActionDict[actionId].StartDateAssigned(newStartDate);
        }
        public void DueDateAssigned(ActionId actionId, DateTime newDueDate)
        {
            ActionDict[actionId].DueDateAssigned(newDueDate);
        }




        
        

        public void Verify(TrustedSystemId id)
        {
            if (Id.Id != id.Id)
                throw new InvalidOperationException();
        }

        public void Create(TrustedSystemId id)
        {
            
        }




        sealed class MutableThought : IItemModel
        {
            public readonly ThoughtId Id;
            public string Subject { get; private set; }
            public readonly DateTime Added;

            public void UpdateSubject(string subject)
            {
                Subject = subject;
            }

            public string UIKey { get { return "thought-" + Id.Id; } }

            public MutableThought(ThoughtId id, string subject, DateTime added)
            {
                Id = id;
                Subject = subject;
                Added = added;
            }

            public string GetTitle()
            {
                return string.Format("Thought '{0}'", Subject);
            }

        }

    }

    

}