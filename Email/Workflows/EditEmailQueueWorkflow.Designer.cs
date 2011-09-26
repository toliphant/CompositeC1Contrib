using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace CompositeC1Contrib.Email.Workflows
{
    partial class EditEmailQueueWorkflow
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            this.SaveCodeActivity = new System.Workflow.Activities.CodeActivity();
            this.elseBranchActivity = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifValidateActivity = new System.Workflow.Activities.IfElseBranchActivity();
            this.setStateActivity2 = new System.Workflow.Activities.SetStateActivity();
            this.ifElseActivity1 = new System.Workflow.Activities.IfElseActivity();
            this.saveHandleExternalEventActivity1 = new Composite.C1Console.Workflow.Activities.SaveHandleExternalEventActivity();
            this.documentFormActivity1 = new Composite.C1Console.Workflow.Activities.DocumentFormActivity();
            this.initCodeActivity = new System.Workflow.Activities.CodeActivity();
            this.setStateActivity1 = new System.Workflow.Activities.SetStateActivity();
            this.cancelHandleExternalEventActivity1 = new Composite.C1Console.Workflow.Activities.CancelHandleExternalEventActivity();
            this.eventDrivenActivity_Save = new System.Workflow.Activities.EventDrivenActivity();
            this.stateInitializationActivity = new System.Workflow.Activities.StateInitializationActivity();
            this.GlobalEventDrivenActivity = new System.Workflow.Activities.EventDrivenActivity();
            this.finalState = new System.Workflow.Activities.StateActivity();
            this.initializationState = new System.Workflow.Activities.StateActivity();
            // 
            // SaveCodeActivity
            // 
            this.SaveCodeActivity.Name = "SaveCodeActivity";
            this.SaveCodeActivity.ExecuteCode += new System.EventHandler(this.saveCodeActivity_ExecuteCode);
            // 
            // elseBranchActivity
            // 
            this.elseBranchActivity.Name = "elseBranchActivity";
            // 
            // ifValidateActivity
            // 
            this.ifValidateActivity.Activities.Add(this.SaveCodeActivity);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.validateSave);
            this.ifValidateActivity.Condition = codecondition1;
            this.ifValidateActivity.Name = "ifValidateActivity";
            // 
            // setStateActivity2
            // 
            this.setStateActivity2.Name = "setStateActivity2";
            this.setStateActivity2.TargetStateName = "initializationState";
            // 
            // ifElseActivity1
            // 
            this.ifElseActivity1.Activities.Add(this.ifValidateActivity);
            this.ifElseActivity1.Activities.Add(this.elseBranchActivity);
            this.ifElseActivity1.Name = "ifElseActivity1";
            // 
            // saveHandleExternalEventActivity1
            // 
            this.saveHandleExternalEventActivity1.EventName = "Save";
            this.saveHandleExternalEventActivity1.InterfaceType = typeof(Composite.C1Console.Workflow.IFormsWorkflowEventService);
            this.saveHandleExternalEventActivity1.Name = "saveHandleExternalEventActivity1";
            // 
            // documentFormActivity1
            // 
            this.documentFormActivity1.ContainerLabel = null;
            this.documentFormActivity1.CustomToolbarDefinitionFileName = "";
            this.documentFormActivity1.FormDefinitionFileName = "\\InstalledPackages\\CompositeC1Contrib.Email\\EditEmailQueue.xml";
            this.documentFormActivity1.Name = "documentFormActivity1";
            // 
            // initCodeActivity
            // 
            this.initCodeActivity.Name = "initCodeActivity";
            this.initCodeActivity.ExecuteCode += new System.EventHandler(this.initCodeActivity_ExecuteCode);
            // 
            // setStateActivity1
            // 
            this.setStateActivity1.Name = "setStateActivity1";
            this.setStateActivity1.TargetStateName = "finalState";
            // 
            // cancelHandleExternalEventActivity1
            // 
            this.cancelHandleExternalEventActivity1.EventName = "Cancel";
            this.cancelHandleExternalEventActivity1.InterfaceType = typeof(Composite.C1Console.Workflow.IFormsWorkflowEventService);
            this.cancelHandleExternalEventActivity1.Name = "cancelHandleExternalEventActivity1";
            // 
            // eventDrivenActivity_Save
            // 
            this.eventDrivenActivity_Save.Activities.Add(this.saveHandleExternalEventActivity1);
            this.eventDrivenActivity_Save.Activities.Add(this.ifElseActivity1);
            this.eventDrivenActivity_Save.Activities.Add(this.setStateActivity2);
            this.eventDrivenActivity_Save.Name = "eventDrivenActivity_Save";
            // 
            // stateInitializationActivity
            // 
            this.stateInitializationActivity.Activities.Add(this.initCodeActivity);
            this.stateInitializationActivity.Activities.Add(this.documentFormActivity1);
            this.stateInitializationActivity.Name = "stateInitializationActivity";
            // 
            // GlobalEventDrivenActivity
            // 
            this.GlobalEventDrivenActivity.Activities.Add(this.cancelHandleExternalEventActivity1);
            this.GlobalEventDrivenActivity.Activities.Add(this.setStateActivity1);
            this.GlobalEventDrivenActivity.Name = "GlobalEventDrivenActivity";
            // 
            // finalState
            // 
            this.finalState.Name = "finalState";
            // 
            // initializationState
            // 
            this.initializationState.Activities.Add(this.stateInitializationActivity);
            this.initializationState.Activities.Add(this.eventDrivenActivity_Save);
            this.initializationState.Name = "initializationState";
            // 
            // EditEmailQueueWorkflow
            // 
            this.Activities.Add(this.initializationState);
            this.Activities.Add(this.finalState);
            this.Activities.Add(this.GlobalEventDrivenActivity);
            this.CompletedStateName = "finalState";
            this.DynamicUpdateCondition = null;
            this.InitialStateName = "initializationState";
            this.Name = "EditEmailQueueWorkflow";
            this.CanModifyActivities = false;

        }

        #endregion

        private EventDrivenActivity GlobalEventDrivenActivity;

        private StateActivity finalState;

        private CodeActivity initCodeActivity;

        private SetStateActivity setStateActivity1;

        private Composite.C1Console.Workflow.Activities.CancelHandleExternalEventActivity cancelHandleExternalEventActivity1;

        private StateInitializationActivity stateInitializationActivity;

        private IfElseBranchActivity elseBranchActivity;

        private IfElseBranchActivity ifValidateActivity;

        private IfElseActivity ifElseActivity1;

        private Composite.C1Console.Workflow.Activities.SaveHandleExternalEventActivity saveHandleExternalEventActivity1;

        private EventDrivenActivity eventDrivenActivity_Save;

        private SetStateActivity setStateActivity2;

        private Composite.C1Console.Workflow.Activities.DocumentFormActivity documentFormActivity1;

        private CodeActivity SaveCodeActivity;

        private StateActivity initializationState;



























    }
}