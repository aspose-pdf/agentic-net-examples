using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputFolder = "InputForms";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    using (Form form = new Form(doc))
                    {
                        form.BindPdf(doc);
                        string guid = Guid.NewGuid().ToString();
                        form.FillField("ProcessedDate", guid);
                    }

                    doc.Save(pdfPath);
                    Console.WriteLine($"Processed {Path.GetFileName(pdfPath)}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {Path.GetFileName(pdfPath)}: {ex.Message}");
            }
        }
    }
}
