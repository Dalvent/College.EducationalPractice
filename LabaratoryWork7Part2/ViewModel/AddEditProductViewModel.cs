using LabaratoryLib;
using LabaratoryWork7Part2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabaratoryWork7Part2.ViewModel
{
    public class AddEditProductViewModel : BaseViewModel
    {
        private readonly Product product;
        private readonly bool isCreatingNew;
        public AddEditProductViewModel(Product product = null)
        {
            if (product == null)
            {
                product = new Product();
                product.Date = DateTime.Now;
                isCreatingNew = true;
            }
            this.product = product;

            SaveCommand = new ActionCommand(Save);
            CancelCommand = new ActionCommand(Cancel);
        }


        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public string Name
        {
            get => product.Name;
            set => product.Name = value;
        }
        public DateTime Date
        {
            get => product.Date;
            set => product.Date = value;
        }
        public decimal Price
        {
            get => product.Price;
            set => product.Price = value;
        }
        public int Count
        {
            get => product.Count;
            set => product.Count = value;
        }

        public void Save()
        {
            OnResult.Invoke(CreateResult(false));
            PageManager.GoBack();
        }

        public void Cancel()
        {
            OnResult.Invoke(CreateResult(true));
            PageManager.GoBack();
        }

        public AddEditProductResult CreateResult(bool IsCancel)
        {
            if (IsCancel)
            {
                return new AddEditProductResult() { IsCancel = true };
            }

            return new AddEditProductResult() {
                IsCreatingNew = this.isCreatingNew,
                ResultProduct = product
            };
        }

        public event Action<AddEditProductResult> OnResult;
    }
}