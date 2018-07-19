using CartaoCredito.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CartaoCredito.Models
{
    public class CartaoCredito
    {
        [Required(ErrorMessage = StringViewModel.MensagemObrigatorio)]
        [Display(Name = "Numero Cartão de Crédito")]
        public string NumeroCartao { get; set; }
    }
}