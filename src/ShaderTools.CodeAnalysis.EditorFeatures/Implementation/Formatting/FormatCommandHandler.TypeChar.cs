﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Threading;
using Microsoft.VisualStudio.Commanding;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Editor.Commanding.Commands;
using ShaderTools.CodeAnalysis.Editor.Properties;
using ShaderTools.CodeAnalysis.Editor.Shared.Extensions;
using ShaderTools.CodeAnalysis.Editor.Shared.Utilities;
using ShaderTools.Utilities.Threading;

namespace ShaderTools.CodeAnalysis.Editor.Implementation.Formatting
{
    internal partial class FormatCommandHandler
    {
        public CommandState GetCommandState(TypeCharCommandArgs args, Func<CommandState> nextHandler)
        {
            return nextHandler();
        }

        public void ExecuteCommand(TypeCharCommandArgs args, Action nextHandler, CommandExecutionContext context)
        {
            ExecuteReturnOrTypeCommand(args, nextHandler, context.OperationContext.UserCancellationToken);
        }

        private bool TryFormat(
            ITextView textView, Document document, IEditorFormattingService formattingService, char typedChar, int position, bool formatOnReturn, CancellationToken cancellationToken)
        {
            var changes = formatOnReturn
                ? formattingService.GetFormattingChangesOnReturnAsync(document, position, cancellationToken).WaitAndGetResult(cancellationToken)
                : formattingService.GetFormattingChangesAsync(document, typedChar, position, cancellationToken).WaitAndGetResult(cancellationToken);

            if (changes == null || changes.Count == 0)
            {
                return false;
            }

            using (var transaction = CreateEditTransaction(textView, EditorFeaturesResources.Automatic_Formatting))
            {
                transaction.MergePolicy = AutomaticCodeChangeMergePolicy.Instance;
                document.Workspace.ApplyTextChanges(document.Id, changes, cancellationToken);
                transaction.Complete();
            }

            return true;
        }

        private CaretPreservingEditTransaction CreateEditTransaction(ITextView view, string description)
        {
            return new CaretPreservingEditTransaction(description, view, _undoHistoryRegistry, _editorOperationsFactoryService);
        }
    }
}
