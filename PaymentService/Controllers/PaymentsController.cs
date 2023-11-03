using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Data;
using PaymentService.Dtos;
using PaymentService.Models;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepo _repository;

        public PaymentsController(IPaymentRepo repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id) 
        {
            var payment = await _repository.GetPaymentAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _repository.GetAllPayments();
            return Ok(payments);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment(PaymentCreateDto paymentCreateDto)
        {
            Console.WriteLine($"--> Creating Payment");
            //user validation
            //order validation
            var paymentModel = _mapper.Map<Payment>(paymentCreateDto);
            paymentModel.PaymentStatus = "Pending";
            paymentModel.Date = DateTime.Now;

            await _repository.AddPaymentAsync(paymentModel);
            return CreatedAtAction(nameof(GetPayment), new {id = paymentModel.Id}, paymentModel);
        }
        [HttpGet("test")]
        public async Task<IActionResult> TestKeyVault()
        {
            Console.WriteLine("--> testing keyvault");
            var keyVaulUrl = "https://hotel-booking-kv.vault.azure.net/";
            var client = new SecretClient(new Uri(keyVaulUrl), new DefaultAzureCredential());
            KeyVaultSecret secret = client.GetSecret("test-secret");
            return Ok(secret.Value);
        }
    }
}
