using ElasticSearch.Api.DTOs;
using ElasticSearch.Api.Models;
using ElasticSearch.Api.Repository;
using System.Net;

namespace ElasticSearch.Api.Services
{
    public class ProductService
    {
        private readonly ProductRepository productRepository;

        public ProductService(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        {

            var response = await productRepository.SaveAsync(request.CreateProduct()); 

            if (response == null) {

                return ResponseDto<ProductDto>.Fail(new List<string> { "hata" }, HttpStatusCode.InternalServerError);
                 
            }

            return ResponseDto<ProductDto>.Success(response.CreateDto(), HttpStatusCode.Created);

        }

        public async Task<ResponseDto<List<ProductDto>>> GetAll()
        {
            var products = await productRepository.GetAll();

          var dto=  products.Select(x => new ProductDto(x.Id,x.Name,x.Price,x.stock,new ProductFeatureDto(x.ProductFeature.Width,x.ProductFeature.Height,x.ProductFeature.Color))).ToList();

            return ResponseDto<List<ProductDto>>.Success(dto, HttpStatusCode.OK);


        }

        public async Task<ResponseDto<ProductDto>> GetById(string id)
        {
            var result = await productRepository.GetById(id);

            return ResponseDto<ProductDto>.Success(result.CreateDto(),HttpStatusCode.OK);
        }
        public async Task<ResponseDto<bool>> Update(ProductUpdateDto updateDto)
        {
            var result = await productRepository.Update(updateDto);

            return ResponseDto<bool>.Success(true, HttpStatusCode.OK);
        }
        public async Task<ResponseDto<bool>> Delete(string id)
        {
            var result = await productRepository.Delete(id);

            return ResponseDto<bool>.Success(true, HttpStatusCode.OK);
        }
    }
}
