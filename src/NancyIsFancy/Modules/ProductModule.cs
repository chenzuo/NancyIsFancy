using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using NancyIsFancy.Models;

namespace NancyIsFancy.Modules
{
    public class ProductModule : NancyModule
    {
        public ProductModule()
        {
            Get["/products/"] = _ =>
                {
                    var query = this.Bind<ProductQuery>();

                    var queryable = Db.Products;

                    if (!string.IsNullOrEmpty(query.Q))
                    {
                        queryable = queryable.Where(x => x.Name.ToLowerInvariant().Contains(query.Q.ToLowerInvariant()));
                    }

                    return queryable;
                };

            Get["/product/{id:int}"] = _ =>
                {
                    var id = (int)_.id;

                    var product = Db.Products.SingleOrDefault(x => x.Id == id);

                    if (product == null)
                    {
                        return HttpStatusCode.NotFound;
                    }

                    return product;
                };

            Post["/product"] = _ =>
                {
                    var model = this.Bind<ProductModel>();

                    var product = new Product
                        {
                            Name = model.Name
                        };

                    Db.Add(product);

                    return new
                        {
                            product.Id
                        };
                };

            Put["/product/{id:int}"] = _ =>
                {
                    var id = (int)_.id;

                    var model = this.Bind<ProductModel>();

                    var product = new Product
                    {
                        Id = id,
                        Name = model.Name
                    };

                    Db.Add(product);

                    return HttpStatusCode.OK;
                };

            Delete["/product/{id:int}"] = _ =>
                {
                    var id = (int)_.id;

                    var product = Db.Products.SingleOrDefault(x => x.Id == id);

                    if (product == null)
                    {
                        return HttpStatusCode.NotFound;
                    }

                    Db.Remove(product);

                    return HttpStatusCode.OK;
                };
        }

        public class ProductModel
        {
            public string Name { get; set; }
        }

        public class ProductQuery
        {
            public string Q { get; set; }   
        }
    }
}