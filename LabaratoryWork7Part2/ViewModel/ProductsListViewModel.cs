using LabaratoryLib;
using LabaratoryWork7Part2.Model;
using LabaratoryWork7Part2.Repository;
using LabaratoryWork7Part2.View;
using LabaratoryWork7Part2.ViewModel.Item;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LabaratoryWork7Part2.ViewModel
{
    public class ProductsListViewModel : BaseViewModel
    {
        private readonly ProductFileRepository productFileRepository;
        public ProductsListViewModel()
        {
            productFileRepository = new ProductFileRepository();
            _ = Initialize();

            NavigateToAddProductCommand = new ActionCommand(NavigateToAddProduct);
            NavigateToEditProductCommand = new ActionCommand(NavigateToEditProduct);
            RemoveProductCommand = new ActionCommand(RemoveProduct);
        }
        public ICommand NavigateToAddProductCommand { get; }
        public ICommand NavigateToEditProductCommand { get; }
        public ICommand RemoveProductCommand { get; }
        private async Task Initialize()
        {
            await UpdateProductsFilter();
        }
        private ProductItemViewModel selecetedProduct;
        public ProductItemViewModel SelecetedProduct
        {
            get => selecetedProduct;
            set
            {
                selecetedProduct = value;
                NotifyPropertyChanged(nameof(SelecetedProduct));
            }
        }
        private ObservableCollection<ProductItemViewModel> filteredProducts = new ObservableCollection<ProductItemViewModel>();
        public ObservableCollection<ProductItemViewModel> FilteredProducts
        {
            get => new ObservableCollection<ProductItemViewModel>(filteredProducts);
            set
            {
                filteredProducts = value;
                NotifyPropertyChanged(nameof(FilteredProducts));
            }
        }
        private string searchWord = "";
        public string SearchWord
        {
            get => searchWord;
            set
            {
                searchWord = value;
                NotifyPropertyChanged(nameof(SearchWord));
                _ = UpdateProductsFilter();
            }
        }
        private DateTime startDateInterval = DateTime.Now.AddYears(-1);
        public DateTime StartDateInterval
        {
            get => startDateInterval;
            set
            {
                startDateInterval = value;
                NotifyPropertyChanged(nameof(StartDateInterval));
                _ = UpdateProductsFilter();
            }
        }
        private DateTime endDateInterval = DateTime.Now.AddDays(10);
        public DateTime EndDateInterval
        {
            get => endDateInterval;
            set
            {
                endDateInterval = value;
                NotifyPropertyChanged(nameof(EndDateInterval));
                _ = UpdateProductsFilter();
            }
        }
        private decimal totalIncome;
        public decimal TotalIncome
        {
            get => totalIncome;
            set
            {
                totalIncome = value;
                NotifyPropertyChanged(nameof(TotalIncome));
            }
        }
        private async Task UpdateProductsFilter()
        {
            Loading = true;
            var products = await productFileRepository.GetValues();
            FilteredProducts = new ObservableCollection<ProductItemViewModel>(products
                .Where(item => item.Name != null && item.Name.ToLower().Contains(SearchWord.ToLower()) && (StartDateInterval <= item.Date && item.Date <= EndDateInterval))
                .OrderBy(item => item.Date)
                .Select(item => new ProductItemViewModel(item)));
            TotalIncome = FilteredProducts.Sum(item => item.Price * item.Count);
            Loading = false;
        }
        private void NavigateToAddProduct()
        {
            var viewModelForResult = new AddEditProductViewModel();
            viewModelForResult.OnResult += OnAddEditProductResult;
            PageManager.NavigateToPage(new AddEditProductView(viewModelForResult));
        }
        private void NavigateToEditProduct()
        {
            if(SelecetedProduct == null)
            {
                MessageBox.Show("Не выбран не один элмент");
                return;
            }
            var viewModelForResult = new AddEditProductViewModel(SelecetedProduct.Product);
            viewModelForResult.OnResult += OnAddEditProductResult;
            PageManager.NavigateToPage(new AddEditProductView(viewModelForResult));
        }
        private async void RemoveProduct()
        {
            if (SelecetedProduct == null)
            {
                MessageBox.Show("Не выбран не один элмент");
                return;
            }
            await productFileRepository.Remove(SelecetedProduct.Product);
            await UpdateProductsFilter();
        }
        private async void OnAddEditProductResult(AddEditProductResult result)
        {
            if (result.IsCancel)
                return;

            if(result.IsCreatingNew)
            {
                await productFileRepository.Add(result.ResultProduct);
            }
            else
            {
                await productFileRepository.Edit(result.ResultProduct);
            }

            await UpdateProductsFilter();
        }
    }
}
