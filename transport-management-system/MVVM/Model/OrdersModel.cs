using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using transport_management_system.Core;
using transport_management_system.Domain.DTO;
using transport_management_system.Infrastructure.Domain;

namespace transport_management_system.MVVM.Model
{
    internal class OrdersModel : ObservableObject
    {
        private ObservableCollection<OrderDTO> _orders;
        public ObservableCollection<OrderDTO> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged("Orders");
            }
        }

        public OrdersModel()
        {
            SetOrders();
        }

        private void SetOrders()
        {
            IEnumerable<Order> orders = OrderRepository.Instance.GetAllOrders();
            ConvertOrdersToOrderDTOs(orders);
        }

        private void ConvertOrdersToOrderDTOs(IEnumerable<Order> fetchedOrders)
        {
            Orders = new ObservableCollection<OrderDTO>();
            if (fetchedOrders.Any())
            {
                foreach (Order fetchedOrder in fetchedOrders)
                {
                    Orders.Add(new OrderDTO(fetchedOrder));
                }
            }
        }

        internal void DeleteOrder(object order)
        {
            if (IsOrder(order))
            {
                int orderId = (int)((OrderDTO)order).Id;
                CarRepository.Instance.RemoveCar(orderId);
                SetOrders();
            }
        }

        private static bool IsOrder(object order)
        {
            return order != null && order is OrderDTO;
        }
    }
}
