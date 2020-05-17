using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SIKONClient.EventHandlers
{
    interface IHandle<T>
    {
        /// <summary>
        /// Opretter et nyt object af typen T
        /// </summary>
        /// <param name="Obj"></param>
        void Create(T Obj);

        /// <summary>
        /// Henter listen af objects type T
        /// </summary>
        /// <returns></returns>
        List<T> Read();

        /// <summary>
        /// Her læser vi fra listen af type T
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        T ReadFrom(int ID);

        /// <summary>
        /// overloader overstående funktion hvis ID er en string, fx email
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        T ReadFrom(string ID);

        /// <summary>
        /// Erstatter objectektet på pågældene ID med et nyt objekt
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="ID"></param>
        bool Update(T Obj, int ID);

        /// <summary>
        ///  Erstatter objectektet på pågældene ID med et nyt objekt. Tager string argument istedet for int, OVERLOAD
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="ID"></param>
        void Update(T Obj, string ID);

        /// <summary>
        /// Sletter pågældene obejct ud fra dets ID (int)
        /// </summary>
        /// <param name="ID"></param>
        void Delete(int ID);

        /// <summary>
        /// Sletter pågældene obejct ud fra dets ID (string)
        /// </summary>
        /// <param name="ID"></param>
        void Delete(string ID);
    }
}
