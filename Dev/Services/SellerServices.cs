using System.Runtime.CompilerServices;
using DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Models;
using Repository;

namespace Services;
public class SellerServices
{
    private readonly ILogger<Seller> _logger;
    private ISellerRepository _repo;

    private IProductRepository _repoProduct;


    public SellerServices(ISellerRepository repo,
                            IProductRepository repoProduct,
                            ILogger<Seller> logger)
    {
        _repo = repo;
        _logger = logger;
        _repoProduct = repoProduct;
    }

    public List<SellerDTO> GetAll()
    {
        try
        {
            return new List<SellerDTO>(_repo.List().Select(s => new SellerDTO(s, true)));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return [];
        }
    }

    public SellerDTO? GetById(int id)
    {
        try
        {
            return new SellerDTO(_repo.GetById(id), true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public SellerDTO? Save(SellerDTO dto)
    {
        try
        {
            Seller entity = (Seller)RuntimeHelpers.GetUninitializedObject(typeof(Seller));

            Seller sellerCreated = _repo.Save(CopyDtoToEntity(dto, entity));

            return new SellerDTO(sellerCreated, true);
        }
        catch (SqlException ex) when (ex.Number == 2601)
        {
            _logger.LogError(ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public void Delete(SellerDTO dto)
    {
        try
        {
            _repo.Delete(_repo.GetById(dto.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }

    public SellerDTO? Update(int id, SellerDTO dto)
    {
        try
        {
            Seller seller = _repo.GetById(id);
            return new SellerDTO(_repo.Update(CopyDtoToEntity(dto, seller)), true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    private Seller CopyDtoToEntity(SellerDTO dto, Seller entity)
    {
        try
        {
            entity.Id = dto.Id;
            entity.Name = dto.Name;
            entity.Email = dto.Email;
            if(dto.Password != null && !dto.Password.Equals("")){
                entity.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password) ;
            }
            entity.Products = [];
            foreach (ProductDTO productDTO in dto.Products)
            {
                Product product = _repoProduct.GetById(productDTO.Id);
                entity.Products.Add(product);
            }

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }

    }
}
