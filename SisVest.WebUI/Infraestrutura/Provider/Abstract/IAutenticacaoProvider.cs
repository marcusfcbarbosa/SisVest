using SisVest.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisVest.WebUI.Infraestrutura.Provider.Abstract
{
    public interface IAutenticacaoProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="autenticacaoModel"></param>
        /// <param name="msgErro"></param>
        /// <returns></returns>
         bool Autenticar(AutenticacaoModel autenticacaoModel, out string msgErro, string grupo = "administrador");

        /// <summary>
        /// 
        /// </summary>
         void Desautenticar();

        /// <summary>
        /// Atributo apenas recuperado
        /// </summary>
         bool Autenticado { get; }

        /// <summary>
        /// Irá retornar uma autenticação
        /// </summary>
         AutenticacaoModel UsuarioAutenticado { get; }
    }
}
