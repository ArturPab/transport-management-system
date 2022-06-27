using System.Windows.Input;
using transport_management_system.Core;
using transport_management_system.MVVM.Model;

namespace transport_management_system.MVVM.ViewModel
{
    internal class OrdersViewModel : ObservableObject
    {
        private NavigationViewModel selectedViewModel;
        public OrdersModel OrdersModel { get; }
        private ICommand _deleteOrderCommand;
        private ICommand _orderFormCommand;
        private ICommand _updateDriversCommand;
        public ICommand DeleteOrderCommand
        {
            get
            {
                if (_deleteOrderCommand == null)
                {
                    _deleteOrderCommand = new RelayCommand(OrdersModel.DeleteOrder);
                }
                return _deleteOrderCommand;
            }
            set
            {
                _deleteOrderCommand = value;
            }
        }

        public ICommand OrderFormCommand
        {
            get
            {
                if (_orderFormCommand == null)
                {
                    _orderFormCommand = new RelayCommand(RouteToOrdersForm);
                }
                return _orderFormCommand;
            }
            set
            {
                _orderFormCommand = value;
            }
        }

        public ICommand UpdateDriversCommand
        {
            get
            {
                if (_updateDriversCommand == null)
                {
                    _updateDriversCommand = new RelayCommand(RouteToAssigningToOrderForm);
                }
                return _updateDriversCommand;
            }
            set
            {
                _updateDriversCommand = value;
            }
        }

        public OrdersViewModel(NavigationViewModel selectedViewModel)
        {
            this.selectedViewModel = selectedViewModel;
            OrdersModel = new();
        }

        private void RouteToOrdersForm(object order)
        {
            selectedViewModel.SelectedViewModel = new OrdersFormViewModel(selectedViewModel, order);
        }

        private void RouteToAssigningToOrderForm(object order)
        {
            selectedViewModel.SelectedViewModel = new AssigningToOrderFormViewModel(selectedViewModel, order);
        }
    }
}
