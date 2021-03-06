﻿using System;
using System.Linq;
using EnvDTE;
using VimSox.Core;

namespace VimSox
{
    public class SolutionExplorerControl : ISolutionExplorerControl
    {
        private readonly UIHierarchy solutionExplorer;
        private readonly ILogger logger;

        internal SolutionExplorerControl(UIHierarchy solutionExplorer, ILogger logger)
        {
            this.solutionExplorer = solutionExplorer;
            this.logger = logger;
        }

        public void MoveDown()
        {
            this.solutionExplorer.SelectDown(vsUISelectionType.vsUISelectionTypeSelect, 1);
        }

        public void MoveLeft()
        {
            var selectedItem = GetSelectedItem(this.solutionExplorer);
            if (selectedItem != null)
            {
                if (selectedItem.UIHierarchyItems.Count > 0 && selectedItem.UIHierarchyItems.Expanded)
                {
                    selectedItem.UIHierarchyItems.Expanded = false;
                }
                else
                {
                    if (selectedItem.Collection.Parent is UIHierarchyItem parent)
                    {
                        parent.Select(vsUISelectionType.vsUISelectionTypeSelect);
                    }
                }
            }
        }

        public void MoveRight()
        {
            if (this.solutionExplorer.UIHierarchyItems.Count > 0)
            {
                var selectedItem = GetSelectedItem(this.solutionExplorer);
                if (selectedItem != null)
                {
                    selectedItem.UIHierarchyItems.Expanded = true;
                }
            }
        }

        public void MoveUp()
        {
            this.solutionExplorer.SelectUp(vsUISelectionType.vsUISelectionTypeSelect, 1);
        }

        public void MoveTop()
        {
            var firstItem = this.solutionExplorer.UIHierarchyItems.OfType<UIHierarchyItem>().FirstOrDefault();
            firstItem?.Select(vsUISelectionType.vsUISelectionTypeSelect);
        }

        public void MoveBottom()
        {
            var lastItem = this.solutionExplorer.UIHierarchyItems.OfType<UIHierarchyItem>().LastOrDefault();
            RecurseMoveBottom(lastItem, this.logger);
        }

        private UIHierarchyItem GetSelectedItem(UIHierarchy solutionExplorer)
        {
            var selectedItems = solutionExplorer.SelectedItems as UIHierarchyItem[];
            return selectedItems?.FirstOrDefault();
        }

        private static void RecurseMoveBottom(UIHierarchyItem item, ILogger logger)
        {
            if (item == null)
            {
                return;
            }

            UIHierarchyItem next = null;
            if (item.UIHierarchyItems.Count > 0 && item.UIHierarchyItems.Expanded)
            {
                next = item.UIHierarchyItems.OfType<UIHierarchyItem>().LastOrDefault();
            }

            logger.Log($"Recursing from {item.Name}, next = {next?.Name ?? string.Empty}");
            if (next != null)
            {
                RecurseMoveBottom(next, logger);
            }
            else
            {
                item.Select(vsUISelectionType.vsUISelectionTypeSelect);
            }
        }
    }
}