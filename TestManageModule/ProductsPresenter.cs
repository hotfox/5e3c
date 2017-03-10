using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teflon.Modules.Interfaces;
using Teflon.SDK.Models;
using Prism.Commands;
using System.Windows.Input;
using System.Windows;
using Teflon.SDK.Events;
using Prism.Events;

namespace Teflon.Modules
{
    [Export(typeof(ProductsPresenter))]
    public class ProductsPresenter: BindableBase
    { 
        [Import(typeof(IDataService))]
        public IDataService  DataService { get; private set; }
        public ICommand EditTestCommand { get; private set; }
        public ObservableCollection<Product> Products
        {
            get
            {
                return new ObservableCollection<Product>(DataService.Products);
            }
        }
        public Product Product { get; set; }
        public Test Test { get; set; }
        [ImportingConstructor]
        public ProductsPresenter()
        {
            InitCommands();
            InitEvents();
        }
        private void InitCommands()
        {
            EditTestCommand = new DelegateCommand(OnEditTestCommand);
        }
        private void InitEvents()
        {

        }
        private void OnEditTestCommand()
        {

        }
    }
}
