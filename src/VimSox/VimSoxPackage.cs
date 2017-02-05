using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VimSox.Core;
using VimSox.Core.Hook;

namespace VimSox
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 1400)] // Info on this package for Help/About
    [Guid(VimSoxPackage.PackageGuidString)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class VimSoxPackage : Package
    {
        /// <summary>
        /// VimSoxPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "13dbe32a-b973-4ef9-8609-4a530ef63131";

        private CommandProcessor commandProcessor;
        private ConditionalKeyHook keyHook;

        /// <summary>
        /// Initializes a new instance of the <see cref="VimSoxPackage"/> class.
        /// </summary>
        public VimSoxPackage()
        {
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            var dte = GetService(typeof(DTE)) as DTE2;

            var logger = new DebugLogger();

            this.commandProcessor = new CommandProcessor(
                new SolutionExplorerControl(dte.ToolWindows.SolutionExplorer, logger),
                logger);

            this.keyHook = new ConditionalKeyHook(
                new SolutionExplorerHookCondition(dte, logger),
                new KeyDispatcher(this.commandProcessor));

            logger.Log("Initialized...");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this.keyHook.Dispose();
        }

        #endregion
    }
}
