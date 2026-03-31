using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "checkboxes.fdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            // Keep only checkbox fields exportable
            foreach (Field field in document.Form.Fields)
            {
                if (field is CheckboxField)
                {
                    ((CheckboxField)field).Exportable = true;
                }
                else
                {
                    field.Exportable = false;
                }
            }

            // Export the exportable fields (checkboxes) to FDF
            using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form(inputPath))
            {
                using (FileStream fdfStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pdfForm.ExportFdf(fdfStream);
                }
            }
        }

        Console.WriteLine("Checkbox fields exported to '" + outputPath + "'.");
    }
}
