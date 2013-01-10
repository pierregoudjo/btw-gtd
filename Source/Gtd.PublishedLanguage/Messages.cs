using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Local
namespace Gtd
{
    #region Generated by Lokad Code DSL
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class ProjectId
    {
        [DataMember(Order = 1)] public Guid Id { get; private set; }
        
        ProjectId () {}
        public ProjectId (Guid id)
        {
            Id = id;
        }
    }
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class ActionId
    {
        [DataMember(Order = 1)] public Guid Id { get; private set; }
        
        ActionId () {}
        public ActionId (Guid id)
        {
            Id = id;
        }
    }
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class CaptureAction : Command, ITenantCommand
    {
        [DataMember(Order = 1)] public TenantId Id { get; private set; }
        [DataMember(Order = 2)] public Guid RequestId { get; private set; }
        [DataMember(Order = 3)] public string Name { get; private set; }
        
        CaptureAction () {}
        public CaptureAction (TenantId id, Guid requestId, string name)
        {
            Id = id;
            RequestId = requestId;
            Name = name;
        }
    }
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class ActionCaptured : Event, ITenantEvent
    {
        [DataMember(Order = 1)] public TenantId Id { get; private set; }
        [DataMember(Order = 2)] public ActionId Action { get; private set; }
        [DataMember(Order = 3)] public string Name { get; private set; }
        [DataMember(Order = 4)] public DateTime TimeUtc { get; private set; }
        
        ActionCaptured () {}
        public ActionCaptured (TenantId id, ActionId action, string name, DateTime timeUtc)
        {
            Id = id;
            Action = action;
            Name = name;
            TimeUtc = timeUtc;
        }
    }
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class CreateProject : Command, ITenantCommand
    {
        [DataMember(Order = 1)] public TenantId Id { get; private set; }
        [DataMember(Order = 2)] public Guid RequestId { get; private set; }
        [DataMember(Order = 3)] public string Name { get; private set; }
        
        CreateProject () {}
        public CreateProject (TenantId id, Guid requestId, string name)
        {
            Id = id;
            RequestId = requestId;
            Name = name;
        }
    }
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class ProjectCreated : Event, ITenantEvent
    {
        [DataMember(Order = 1)] public TenantId Id { get; private set; }
        [DataMember(Order = 2)] public ProjectId Project { get; private set; }
        [DataMember(Order = 3)] public string Name { get; private set; }
        [DataMember(Order = 4)] public DateTime TimeUtc { get; private set; }
        
        ProjectCreated () {}
        public ProjectCreated (TenantId id, ProjectId project, string name, DateTime timeUtc)
        {
            Id = id;
            Project = project;
            Name = name;
            TimeUtc = timeUtc;
        }
    }
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class ActionAssignedToProject : Event, ITenantEvent
    {
        [DataMember(Order = 1)] public TenantId Id { get; private set; }
        [DataMember(Order = 2)] public ActionId Action { get; private set; }
        [DataMember(Order = 3)] public ProjectId NewProject { get; private set; }
        [DataMember(Order = 4)] public DateTime TimeUtc { get; private set; }
        
        ActionAssignedToProject () {}
        public ActionAssignedToProject (TenantId id, ActionId action, ProjectId newProject, DateTime timeUtc)
        {
            Id = id;
            Action = action;
            NewProject = newProject;
            TimeUtc = timeUtc;
        }
    }
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class ActionRemovedFromProject : Event, ITenantEvent
    {
        [DataMember(Order = 1)] public TenantId Id { get; private set; }
        [DataMember(Order = 2)] public ActionId Action { get; private set; }
        [DataMember(Order = 3)] public ProjectId OldProject { get; private set; }
        [DataMember(Order = 4)] public DateTime TimeUtc { get; private set; }
        
        ActionRemovedFromProject () {}
        public ActionRemovedFromProject (TenantId id, ActionId action, ProjectId oldProject, DateTime timeUtc)
        {
            Id = id;
            Action = action;
            OldProject = oldProject;
            TimeUtc = timeUtc;
        }
    }
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class ActionMovedToProject : Event, ITenantEvent
    {
        [DataMember(Order = 1)] public TenantId Id { get; private set; }
        [DataMember(Order = 2)] public ActionId Action { get; private set; }
        [DataMember(Order = 3)] public ProjectId OldProject { get; private set; }
        [DataMember(Order = 4)] public ProjectId NewProject { get; private set; }
        [DataMember(Order = 5)] public DateTime TimeUtc { get; private set; }
        
        ActionMovedToProject () {}
        public ActionMovedToProject (TenantId id, ActionId action, ProjectId oldProject, ProjectId newProject, DateTime timeUtc)
        {
            Id = id;
            Action = action;
            OldProject = oldProject;
            NewProject = newProject;
            TimeUtc = timeUtc;
        }
    }
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class RemoveAction : Command, ITenantCommand
    {
        [DataMember(Order = 1)] public TenantId Id { get; private set; }
        [DataMember(Order = 2)] public ActionId Action { get; private set; }
        
        RemoveAction () {}
        public RemoveAction (TenantId id, ActionId action)
        {
            Id = id;
            Action = action;
        }
    }
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class ActionRemoved : Event, ITenantEvent
    {
        [DataMember(Order = 1)] public TenantId Id { get; private set; }
        [DataMember(Order = 2)] public ActionId Action { get; private set; }
        [DataMember(Order = 3)] public DateTime TimeUtc { get; private set; }
        
        ActionRemoved () {}
        public ActionRemoved (TenantId id, ActionId action, DateTime timeUtc)
        {
            Id = id;
            Action = action;
            TimeUtc = timeUtc;
        }
    }
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class ActionRenamed : Event, ITenantEvent
    {
        [DataMember(Order = 1)] public TenantId Id { get; private set; }
        [DataMember(Order = 2)] public ActionId Action { get; private set; }
        [DataMember(Order = 3)] public string Name { get; private set; }
        [DataMember(Order = 4)] public DateTime TimeUtc { get; private set; }
        
        ActionRenamed () {}
        public ActionRenamed (TenantId id, ActionId action, string name, DateTime timeUtc)
        {
            Id = id;
            Action = action;
            Name = name;
            TimeUtc = timeUtc;
        }
    }
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class CompleteAction : Command, ITenantCommand
    {
        [DataMember(Order = 1)] public TenantId Id { get; private set; }
        [DataMember(Order = 2)] public ActionId Action { get; private set; }
        
        CompleteAction () {}
        public CompleteAction (TenantId id, ActionId action)
        {
            Id = id;
            Action = action;
        }
    }
    [DataContract(Namespace = "BTW2/GTD")]
    public partial class ActionCompleted : Event, ITenantEvent
    {
        [DataMember(Order = 1)] public TenantId Id { get; private set; }
        [DataMember(Order = 2)] public ActionId Action { get; private set; }
        [DataMember(Order = 3)] public DateTime TimeUtc { get; private set; }
        
        ActionCompleted () {}
        public ActionCompleted (TenantId id, ActionId action, DateTime timeUtc)
        {
            Id = id;
            Action = action;
            TimeUtc = timeUtc;
        }
    }
    
    public interface ITenantApplicationService
    {
        void When(CaptureAction c);
        void When(CreateProject c);
        void When(RemoveAction c);
        void When(CompleteAction c);
    }
    
    public interface ITenantState
    {
        void When(ActionCaptured e);
        void When(ProjectCreated e);
        void When(ActionAssignedToProject e);
        void When(ActionRemovedFromProject e);
        void When(ActionMovedToProject e);
        void When(ActionRemoved e);
        void When(ActionRenamed e);
        void When(ActionCompleted e);
    }
    #endregion
}
