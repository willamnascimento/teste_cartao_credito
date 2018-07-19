using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CartaoCredito.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ValidaCartaoCredito(Models.CartaoCredito cartao_credito)
        {

            // remove espaços vazios
            var temp = cartao_credito.NumeroCartao.Replace(" ", "");
            // arrays para armazenar os valores
            var sequencia_invertida = new List<int>();
            var sequencia_dobro = new List<int>();
            var soma = 0;
            var retorno = string.Empty;

            retorno += IdentificaTipoCartao(temp);

            var is_valid = ValidaTamanho(retorno, temp);

            if (is_valid)
            {
                // inverte a sequencia do cartão
                for (int i = temp.Length; i > 0; i--)
                    sequencia_invertida.Add(Convert.ToInt32(temp[i - 1].ToString()));

                // efetua o dobro da sequencia alternada começando pela segunda posição
                for (int i = 0; i < sequencia_invertida.Count; i++)
                {
                    if (i % 2 != 0)
                    {
                        var valor = sequencia_invertida[i] * 2;
                        if (valor > 9)
                        {
                            var v = valor.ToString();
                            var soma_digito = 0;
                            foreach (var item in v)
                                soma_digito += Convert.ToInt32(item.ToString());
                            sequencia_dobro.Add(soma_digito);
                        }
                        else
                        {
                            sequencia_dobro.Add(valor);
                        }
                    }
                    else
                    {
                        sequencia_dobro.Add(sequencia_invertida[i]);
                    }
                }

                // soma da sequencia em dobro alternada
                foreach (var item in sequencia_dobro)
                    soma += item;

                if (soma % 10 == 0)
                    retorno += temp + " (válido)";
                else
                    retorno += temp + " (inválido)";
            }
            else
            {
                retorno += temp + " (inválido)";
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        private string IdentificaTipoCartao(string numero_cartao)
        {
            // verifica se o cartao é visa
            var digito_visa = numero_cartao.Substring(0, 1);
            if (digito_visa == "4")
                return "VISA: ";

            // verifica se o cartao é mastercard
            var digito_mastercard = Convert.ToInt32(numero_cartao.Substring(0, 2));
            if (digito_mastercard >= 51 && digito_mastercard <= 55)
                return "MasterCard: ";

            // verifica se o cartao é amex
            var digito_amex = numero_cartao.Substring(0, 2);
            if (digito_amex == "34" || digito_amex == "37")
                return "AMEX: ";

            // verifica se o cartao é visa
            var digito_discover = numero_cartao.Substring(0, 4);
            if (digito_discover == "6011")
                return "Discover: ";

            
            return "Desconhecido: ";
        }

        public bool ValidaTamanho(string tipo, string numero_cartao)
        {
            var retorno = false;
            if (tipo.Contains("VISA"))
            {
                if (numero_cartao.Length == 13 || numero_cartao.Length == 16)
                    retorno = true;
            }else if (tipo.Contains("AMEX"))
            {
                if (numero_cartao.Length == 15)
                    retorno = true;
            }else if (tipo.Contains("MasterCard"))
            {
                if (numero_cartao.Length == 16)
                    retorno = true;
            }else if (tipo.Contains("Discover"))
            {
                if (numero_cartao.Length == 16)
                    retorno = true;
            }
            return retorno;
        }

    }
}