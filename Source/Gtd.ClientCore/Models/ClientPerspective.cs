﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using Gtd.Shell.Filters;
using System.Linq;

namespace Gtd.Client.Models
{
    public sealed class ClientPerspective 
    {
        public ClientModel Model { get; private set; }
        public IFilterCriteria CurrentFilter { get; private set; }

        public ClientPerspective()
        {
            CurrentFilter = new AllActionsFilter();
        }

        public void SwitchToModel(ClientModel model)
        {
            Model = model;
        }


        public IList<ImmutableProject> ListProjects()
        {
            return Model.ListProjects();
        }

       


        

        public FilteredProject GetProject(ProjectId id)
        {
            var pid = Model.GetProjectOrNull(id);

            var actions = CurrentFilter.FilterActions(pid).ToList().AsReadOnly();
            var count = CurrentFilter.FormatActionCount(actions.Count);
            return new FilteredProject(pid.Info, actions, count);
        }


        public void SwitchToFilter(IFilterCriteria criteria)
        {
            this.CurrentFilter = criteria;
        }
    }

    public sealed class ImmutableInbox
    {
        readonly IImmutableDictionary<StuffId, ImmutableStuff> _dict;
        public ImmutableInbox(IImmutableDictionary<StuffId, ImmutableStuff> dict)
        {
            _dict = dict;
        }

        public int Count { get { return _dict.Count; } }
        public IOrderedEnumerable<ImmutableStuff> GetStuffOrdered()
        {
            return _dict.Values.OrderBy(i => i.Order);
        }
        
    }

    public sealed class ImmutableClientModel
    {
        public readonly ImmutableInbox Inbox;
        public readonly IImmutableList<ImmutableProject> Projects;

        public ImmutableClientModel(ImmutableInbox inbox, IImmutableList<ImmutableProject> projects)
        {
            Inbox = inbox;
            Projects = projects;
        }
    }

    public sealed class ImmutableStuff
    {
        public readonly StuffId StuffId;
        public readonly string Description;
        public readonly string UIKey;
        public readonly uint Order;

        public ImmutableStuff WithDescription(string descriptionOfStuff)
        {
            return new ImmutableStuff(StuffId, descriptionOfStuff, UIKey,Order);
        }

        public ImmutableStuff(StuffId stuffId, string descriptionOfStuff, string uiKey, uint order)
        {
            StuffId = stuffId;
            Description = descriptionOfStuff;
            UIKey = uiKey;
            Order = order;
        }
    }

    public sealed class ImmutableAction
    {
        public readonly ActionId ActionId;
        public readonly string Outcome;
        public readonly bool Completed;
        public readonly bool Archived;
        public readonly ProjectId ProjectId;
        public readonly DateTime StartDate;
        public readonly DateTime DueDate;
        public readonly string UIKey;

        public ImmutableAction(string uiKey, ActionId actionId, string outcome, bool completed, bool archived, ProjectId projectId, DateTime startDate, DateTime dueDate)
        {
            UIKey = uiKey;
            ActionId = actionId;
            Outcome = outcome;
            Completed = completed;
            Archived = archived;
            ProjectId = projectId;
            StartDate = startDate;
            DueDate = dueDate;
        }
    }

    public sealed class ImmutableProject
    {
        public readonly ImmutableProjectInfo Info;
        public readonly ReadOnlyCollection<ImmutableAction> Actions;

        public ImmutableProject(ImmutableProjectInfo info, ReadOnlyCollection<ImmutableAction> actions)
        {
            Info = info;
            Actions = actions;
        }
    }

    public sealed class ImmutableProjectInfo
    {
        public readonly ProjectId ProjectId;
        public readonly string Outcome;
        public readonly ProjectType Type;
        public readonly string UIKey;

        public ImmutableProjectInfo(ProjectId projectId, string outcome, ProjectType type, string uiKey)
        {
            ProjectId = projectId;
            Outcome = outcome;
            Type = type;
            UIKey = uiKey;
        }
    }

    public sealed class FilteredProject
    {
        public readonly ImmutableProjectInfo Info;
        public readonly ReadOnlyCollection<ImmutableAction> FilteredActions;
        public readonly string ActionCount;

        public FilteredProject(ImmutableProjectInfo info, ReadOnlyCollection<ImmutableAction> filteredActions, string actionCount)
        {
            Info = info;
            FilteredActions = filteredActions;
            ActionCount = actionCount;
        }
    }
}