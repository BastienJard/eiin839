using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace WebDynamic
{
    class Mymethods
    {
        //Réponse pour la question 4
        //Exemple d'URL à tester : http://localhost:8080/bienvenue/a/Polytech/MyMethod?param1=Ladies&param2=Gentleman
        public string MyMethod(string param1, string param2)
        {
            return "<HTML><BODY> Hello " + param1 + " et " + param2 + "</BODY></HTML>";//Return le contenu de la page web avec les paramètres
        }
        //Réponse pour la question 4
        //Exemple d'URL à tester : http://localhost:8080/bienvenue/a/Polytech/MethodExtern?param1=Ladies&param2=Gentleman
        public string MethodExtern(string param1, string param2)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"F:\SI4\SOC\eiin839\TD2\MethodExtern\bin\Debug\netcoreapp3.1\MethodExtern.exe";//Lien local de l'éxecutable à éxecuter
            start.Arguments = param1 + " " + param2;//Ajout des arguments pour l'exécutable
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))//Appel de l'exécutable
            {
                using (StreamReader reader = process.StandardOutput)//Redirection de la sortie standard de l'URL
                {
                    string result = reader.ReadToEnd();//Lecture de la sortie standard
                    Console.WriteLine(result);
                    return result;//Return ce qu'affiche l'exécutable
                }
            }
        }
    }
}
