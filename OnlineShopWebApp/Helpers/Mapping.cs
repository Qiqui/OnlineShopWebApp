using Microsoft.AspNetCore.Identity;
using OnlineShop.Domain.Entities;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {
        public static List<ProductViewModel> ToProductsViewModel(this List<Product> products)
        {
            var productsViewModels = new List<ProductViewModel>();
            foreach (var product in products)
            {
                productsViewModels.Add(ToProductViewModel(product));
            }

            return productsViewModels;
        }

        public static ProductViewModel ToProductViewModel(this Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                Gender = product.Gender,
                Brand = product.Brand,
                Collection = product.Collection,
                Color = product.Color,
                Material = product.Material,
                ImagePaths = product.ImagePaths
            };
        }

        public static ProductUpdateViewModel ToProductUpdateViewModel(this Product product)
        {
            return new ProductUpdateViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                Gender = product.Gender,
                Brand = product.Brand,
                Collection = product.Collection,
                Color = product.Color,
                Material = product.Material
            };
        }

        public static Product ToProduct(this ProductUpdateViewModel productVM)
        {
            return new Product
            {
                Name = productVM.Name,
                Cost = productVM.Cost,
                Description = productVM.Description,
                Gender = productVM.Gender,
                Brand = productVM.Brand,
                Collection = productVM.Collection,
                Color = productVM.Color,
                Material = productVM.Material
            };
        }

        public static Product ToProduct(this ProductAddViewModel productVM)
        {
            return new Product
            {
                Name = productVM.Name,
                Cost = productVM.Cost,
                Description = productVM.Description,
                Gender = productVM.Gender,
                Brand = productVM.Brand,
                Collection = productVM.Collection,
                Color = productVM.Color,
                Material = productVM.Material,
            };
        }

        public static Product ToProduct(this ProductAddViewModel productVM, List<string> ImagesPath)
        {
            return new Product
            {
                Name = productVM.Name,
                Cost = productVM.Cost,
                Description = productVM.Description,
                Gender = productVM.Gender,
                Brand = productVM.Brand,
                Collection = productVM.Collection,
                Color = productVM.Color,
                Material = productVM.Material,
                ImagePaths = ImagesPath
            };
        }

        public static CartViewModel ToCartViewModel(this Cart cartDb)
        {
            if (cartDb == null)
                return null;

            return new CartViewModel
            {
                Id = cartDb.Id,
                Positions = ToCartPositionsViewModel(cartDb.Positions)
            };
        }

        public static List<CartPositionViewModel> ToCartPositionsViewModel(this List<CartPosition> cartDbPositions)
        {
            var cartPositions = new List<CartPositionViewModel>();
            foreach (var cartDbPosition in cartDbPositions)
            {
                var cartPosition = new CartPositionViewModel
                {
                    Id = cartDbPosition.Id,
                    Quantity = cartDbPosition.Quantity,
                    Product = ToProductViewModel(cartDbPosition.Product),
                };

                cartPositions.Add(cartPosition);
            }

            return cartPositions;
        }

        public static List<CartPositionViewModel> ToCartPositionsViewModel(this List<OrderPosition> orderPositions)
        {
            var cartPositions = new List<CartPositionViewModel>();
            foreach (var orderPosition in orderPositions)
            {
                var cartPosition = new CartPositionViewModel
                {
                    Id = orderPosition.Id,
                    Quantity = orderPosition.Quantity,
                    Product = ToProductViewModel(orderPosition.Product),
                };

                cartPositions.Add(cartPosition);
            }

            return cartPositions;
        }

        public static CompareViewModel ToCompareViewModel(this Compare compare)
        {
            return new CompareViewModel
            {
                Id = compare.Id,
                Products = ToProductsViewModel(compare.Products)
            };
        }

        public static FavouritesViewModel ToFavouritesViewModel(this Favourites favourites)
        {
            return new FavouritesViewModel
            {
                Id = favourites.Id,
                Products = ToProductsViewModel(favourites.Products)
            };
        }

        public static Order ToOrder(this OrderViewModel order)
        {
            return new Order
            {
                ContactInfo = order.ContactInfo.ToContactInfo(),
                CreateDate = order.CreateDate,
                Status = order.Status,
            };
        }

        public static OrderViewModel ToOrderViewModel(this Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                UserId = order.User.Id,
                Number = order.Number,
                ContactInfo = order.ContactInfo.ToContactInfoViewModel(),
                CreateDate = order.CreateDate,
                Status = order.Status,
                Positions = order.Positions.ToCartPositionsViewModel()
            };
        }

        public static List<OrderViewModel> ToOrdersViewModel(this List<Order> orders)
        {
            var orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                orderViewModels.Add(order.ToOrderViewModel());
            }

            return orderViewModels;
        }

        public static List<OrderPosition> ToOrderPositions(this List<CartPosition> positions)
        {
            var orderPositions = new List<OrderPosition>();
            foreach (var position in positions)
            {
                var orderPosition = new OrderPosition
                {
                    Quantity = position.Quantity,
                    Product = position.Product
                };

                orderPositions.Add(orderPosition);
            }

            return orderPositions;
        }

        public static ContactInfo ToContactInfo(this ContactInfoViewModel contactInfoVM)
        {
            return new ContactInfo()
            {
                Name = contactInfoVM.Name,
                Surname = contactInfoVM.Surname,
                Email = contactInfoVM.Email,
                Address = contactInfoVM.Address,
                Phone = contactInfoVM.Phone,
                IsAgreeWithDataProcessing = contactInfoVM.IsAgreeWithDataProcessing
            };
        }

        public static ContactInfoViewModel ToContactInfoViewModel(this ContactInfo contactInfo)
        {
            return new ContactInfoViewModel()
            {
                Name = contactInfo.Name,
                Surname = contactInfo.Surname,
                Email = contactInfo.Email,
                Address = contactInfo.Address,
                Phone = contactInfo.Phone,
                IsAgreeWithDataProcessing = contactInfo.IsAgreeWithDataProcessing
            };
        }

        public static User ToUser(this UserViewModel userVM)
        {
           return new User
            {
                Email = userVM.Email,
                UserName = userVM.Email,
                Name = userVM.Name,
                Surname = userVM.Surname,
                Age = userVM.Age,
                PhoneNumber = userVM.Phone,
            };
        }

        public static UserViewModel ToUserViewModel(this User user)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Age = user.Age,
                Phone = user.PhoneNumber,
                Email = user.Email,
                ImagePath = user.ImagePath
            };
        }

        public static UserUpdateViewModel ToUserUpdateViewModel(this User user)
        {
            return new UserUpdateViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Age = user.Age,
                Phone = user.PhoneNumber,
                Email = user.Email,
                ImagePath = user.ImagePath
            };
        }

        public static User ToUser(this UserUpdateViewModel userVM)
        {
            return new User
            {
                Id = userVM.Id,
                Name = userVM.Name,
                Email = userVM.Email,
                UserName = userVM.Email,
                Surname = userVM.Surname,
                Age = userVM.Age,
                PhoneNumber = userVM.Phone
            };
        }

        public static List<UserViewModel> ToUsersViewModel(this List<User> users)
        {
            var usersVM = new List<UserViewModel>();
            foreach (var user in users)
            {
                usersVM.Add(user.ToUserViewModel());
            }

            return usersVM;
        }

        public static void UpdateByUserViewModel(this User user, UserUpdateViewModel userUpdateVM)
        {
            user.Name = userUpdateVM.Name;
            user.Surname = userUpdateVM.Surname;
            user.Age = userUpdateVM.Age;
            user.PhoneNumber = userUpdateVM.Phone;
            user.Email = userUpdateVM.Email;
        }

        public static RoleViewModel ToRoleViewModel(this IdentityRole role)
        {
            var roleVM = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };

            return roleVM;
        }
            
        public static List<RoleViewModel> ToRolesViewModel(this List<IdentityRole> roles)
        {
            var rolesVM = new List<RoleViewModel>();
            foreach(var role in roles)
            {
                var roleVM = role.ToRoleViewModel();
                rolesVM.Add(roleVM);
            }

            return rolesVM;
        }
    }
}
