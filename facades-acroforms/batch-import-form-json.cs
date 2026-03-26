using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

public class BatchFormImporter
{
    public static void Main()
    {
        string inputDirectory = "pdfs";
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine("Input directory not found: " + inputDirectory);
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            string jsonPath = Path.ChangeExtension(pdfPath, ".json");
            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine("JSON file missing for: " + pdfPath);
                return;
            }

            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                string outputFileName = Path.GetFileNameWithoutExtension(pdfPath) + "_filled.pdf";
                using (Form form = new Form(pdfPath, outputFileName))
                {
                    form.ImportJson(jsonStream);
                    form.Save();
                }
            }

            Console.WriteLine("Processed: " + Path.GetFileName(pdfPath));
        });
    }
}