using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Core.Presentation;
using System.Activities.Statements;
using System.Activities.Presentation.Toolbox;
using System.Activities.XamlIntegration;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Teflon.SDK.Interfaces;
using System.Activities.Expressions;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xaml;

namespace Teflon.Modules
{
    /// <summary>
    /// DesignView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(DesignView))]
    public partial class DesignView : System.Windows.Controls.UserControl
    {
        public DesignView()
        {
            InitializeComponent();

        }
        [ImportingConstructor]
        public DesignView(DesignPresenter presenter) : this()
        {
            DataContext = presenter;
            this.presenter = presenter;

            DemoForNI6001();
        }

        public void OnNewTestItemEvent()
        {
            var collection = presenter.Designtime.GetDeviceOperatipons(presenter.ProductName);
            ToolboxCategory category = new ToolboxCategory(presenter.ProductName);
            foreach (Activity a in collection)
            {
                category.Add(new ToolboxItemWrapper(a.GetType()));
            }
            ToolboxControl.Categories.Add(category);
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            // register metadata
            (new DesignerMetadata()).Register();

            // create the workflow designer
            WorkflowDesigner wd = new WorkflowDesigner();
            designer = wd;
           // wd.Load(new Flowchart());
            DesignerBorder.Child = wd.View;
            PropertyBorder.Child = wd.PropertyInspectorView;
        }

        private void DemoForNI6001()
        {
            LoadOperations("NICard");
            LoadOperations("General");
        }

        private void LoadOperations(string deviceType)
        {
            var operatipons = presenter.Designtime.GetDeviceOperatipons(deviceType);
            ToolboxCategory category = new ToolboxCategory(deviceType);
            foreach (IOperation op in operatipons)
            {
                category.Add(new ToolboxItemWrapper(op.GetType()));
            }
            ToolboxControl.Categories.Add(category);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Test File|*.test";
            if(dialog.ShowDialog()==DialogResult.OK)
            {
                designer.Flush();
                string text = Encrypt.EncryptString(designer.Text, "hotdog");
                File.WriteAllText(dialog.FileName,text);
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Test File|*test";
            if(dialog.ShowDialog()==DialogResult.OK)
            {
                string pure_text = File.ReadAllText(dialog.FileName);
                string text = Encrypt.DecryptString(pure_text, "hotdog");
                var root = XamlServices.Load(ActivityXamlServices.CreateReader
                                            (new XamlXmlReader(new StringReader(text),
                                             new XamlXmlReaderSettings { LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly() })));
                designer.Load(root);
            }
        }
        private DesignPresenter presenter;
        private WorkflowDesigner designer;
    }
}
