using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputDirectory = "pdfs";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine("Input directory not found: " + inputDirectory);
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputFileName = "decorated_" + fileName;

            using (FormEditor editor = new FormEditor(pdfPath, outputFileName))
            {
                editor.Facade = new FormFieldFacade();
                editor.Facade.Alignment = FormFieldFacade.AlignCenter;
                editor.DecorateField(FieldType.CheckBox);
            }

            Console.WriteLine("Processed: " + fileName + " -> " + outputFileName);
        }
    }
}