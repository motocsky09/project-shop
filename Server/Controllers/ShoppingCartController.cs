using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Entities;
using Server.Repositories;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IShoppingCartRepository _shoppingcartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartController(IConfiguration configuration, IShoppingCartRepository shoppingcartRepository, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _shoppingcartRepository = shoppingcartRepository;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpGet]
        [Route("GetShoppingCartById")]
        public ActionResult GetShoppingCartById(string shoppingcartId)
        {
            var result = _shoppingcartRepository.GetShoppingCartById(shoppingcartId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetShoppingCarts")]
        public ActionResult GetShoppingCarts()
        {
            var result = _shoppingcartRepository.GetShoppingCarts();
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateShoppingCart")]
        public ActionResult CreateShoppingCart(ShoppingCart shoppingCart)
        {
            _shoppingcartRepository.CreateShoppingCart(shoppingCart);
            return Ok(shoppingCart);
        }

        [HttpPut]
        [Route("UpdateShoppingCart")]
        public ActionResult UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            _shoppingcartRepository.UpdateShoppingCart(shoppingCart);
            return Ok(shoppingCart);
        }

        [HttpDelete]
        [Route("DeleteShoppingCart")]
        public ActionResult DeleteShoppingCart(string shoppingcartId)
        {
            _shoppingcartRepository.DeleteShoppingCart(shoppingcartId);
            return Ok();
        }

        [HttpPost]
        [Route("CreateFirstShoppingCartByUsername")]
        public ActionResult CreateFirstShoppingCartByUsername([FromQuery]string userName)
        {
            var res = _shoppingcartRepository.CreateFirstShoppingCartByUsername(userName);
            return Ok(res);
        }
        
        [HttpPost]
        [Route("AddProductInShoppingCart")]
        public ActionResult AddProductInShoppingCart(string shoppingCartId, int productId, int selectedQuantity)
        {
            var res = _shoppingcartRepository.AddProductInShoppingCart(shoppingCartId, productId, selectedQuantity);
            return Ok(res);
        }

        [HttpPut("UpdateProductInShoppingCart")]
        public IActionResult UpdateProductInShoppingCart([FromQuery] string shoppingCartId, [FromQuery] int productId, [FromQuery] int updatedQuantity)
        {
            try
            {
                // Apelăm metoda din repository pentru a actualiza produsul
                var updatedProduct = _shoppingcartRepository.UpdateProductInShoppingCart(shoppingCartId, productId, updatedQuantity);

                // Dacă nu a fost găsit produsul, returnăm NotFound
                if (updatedProduct == null)
                {
                    return NotFound(new { message = "Product not found in shopping cart" });
                }

                // Returnăm produsul actualizat
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                // Returnăm o eroare în caz de excepție
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetShoppingCartListById")]
        public ActionResult GetShoppingCartListById([FromQuery]string shoppingCartId)
        {
            var res = _shoppingcartRepository.GetShoppingCartListById(shoppingCartId);
            return Ok(res);
        }

        [HttpGet]
        [Route("GetProdutsFromShoppingById")]
        public ActionResult GetProdutsFromShoppingById([FromQuery] string shoppingCartId)
        {
            var res = _shoppingcartRepository.GetProdutsFromShoppingById(shoppingCartId);

            return Ok(res);
        }


        [HttpGet]
        [Route("GetCountProductsFromCartShopping")]
        public ActionResult GetCountProductsFromCartShopping([FromQuery] string shoppingCartId)
        {
            var res = _shoppingcartRepository.GetCountProductsFromCartShopping(shoppingCartId);

            return Ok(res);
        }

        [HttpDelete]
        [Route("DeleteProductFromCart")]
        public ActionResult DeleteProductFromCart(string shoppingCartId, int productId)
        {
            _shoppingcartRepository.DeleteProductFromCart(shoppingCartId, productId);
            return Ok();
        }
    }
}
