using CompetitionJudo.Data;
using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionJudo.Business.File_Import
{
    public class ExcelImport
    {
        public async Task<List<Competiteur>> ImporterCSV(string cheminFichier)
        {
            List<Competiteur> ListeResult = new List<Competiteur>();

            string line;
            using (StreamReader reader = new StreamReader(cheminFichier, Encoding.GetEncoding(1252)))
            {
                string headerLine = await reader.ReadLineAsync();
                while (!reader.EndOfStream)
                {
                    try
                    {
                        line = reader.ReadLine();
                        var values = line.Split(';');

                        if (!String.IsNullOrWhiteSpace(values[0]) && !String.IsNullOrWhiteSpace(values[1]) && !String.IsNullOrWhiteSpace(values[5]))
                        {
                            Competiteur competiteurTemporaire = new Competiteur
                            {
                                Nom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(values[0].ToLower()),
                                Prenom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(values[1].ToLower()),
                                Club = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(values[4].ToLower()),
                            };

                            Sexes sexOut;
                            if (Enum.TryParse(values[2], out sexOut))
                            {
                                competiteurTemporaire.Sexe = (Sexes)Enum.Parse(typeof(Sexes), values[2]);
                            }

                            double doubleOut;
                            if (double.TryParse(values[3], out doubleOut))
                            {
                                competiteurTemporaire.Poids = Convert.ToDouble(values[3]);
                            }
                            else
                            {
                                competiteurTemporaire.Poids = 0;
                            }

                            Categories categoriesOut;
                            if (Enum.TryParse(values[5], out categoriesOut))
                            {
                                competiteurTemporaire.Categorie = (Categories)Enum.Parse(typeof(Categories), values[5]);
                            }
                            ListeResult.Add(competiteurTemporaire);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            return ListeResult;
        }

        public List<Competiteur> ImporterXLS(string cheminFichier)
        {
            List<Competiteur> listeResult = new List<Competiteur>();

            List<string> rejectList = new List<string>();

            using (Stream stream = new FileStream(cheminFichier,
                                 FileMode.Open,
                                 FileAccess.Read,
                                 FileShare.ReadWrite))
            {
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                excelReader.IsFirstRowAsColumnNames = true;

                //Skip First row
                excelReader.Read();
                while (excelReader.Read())
                {
                    Competiteur competiteurTemporaire = null;
                    try
                    {
                        competiteurTemporaire = new Competiteur
                        {
                            Nom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(excelReader.GetString(0).ToLower()),
                            Prenom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(excelReader.GetString(1).ToLower()),
                            Club = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(excelReader.GetString(4).ToLower()),
                        };

                        Sexes sexOut;
                        if (Enum.TryParse(excelReader.GetString(2), out sexOut))
                        {
                            competiteurTemporaire.Sexe = (Sexes)Enum.Parse(typeof(Sexes), excelReader.GetString(2));
                        }

                        double doubleOut;
                        if (double.TryParse(excelReader.GetString(3), out doubleOut))
                        {
                            competiteurTemporaire.Poids = Convert.ToDouble(excelReader.GetString(3));
                        }
                        else
                        {
                            competiteurTemporaire.Poids = 0;
                        }

                        Categories categoriesOut;
                        if (Enum.TryParse(excelReader.GetString(5), out categoriesOut))
                        {
                            competiteurTemporaire.Categorie = (Categories)Enum.Parse(typeof(Categories), excelReader.GetString(5));

                            if (competiteurTemporaire.Categorie == Categories.Tous)
                            {
                                throw new Exception($"Catégorie mal formattée : {excelReader.GetString(5)}");
                            }
                        }
                        else
                        {
                            throw new Exception($"Catégorie mal formattée : {excelReader.GetString(5)}");
                        }
                        if (competiteurTemporaire != null)
                        {
                            listeResult.Add(competiteurTemporaire);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            
            return listeResult;
        }
    }
}
