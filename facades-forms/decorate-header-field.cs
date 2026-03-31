using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string headerImagePath = "header.jpg";
        const string intermediatePath = "temp.pdf";
        const string outputPath = "decorated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        // Add an image as a page header.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);
        fileStamp.AddHeader(headerImagePath, 20f);
        fileStamp.Save(intermediatePath);
        fileStamp.Close();

        // Center‑align the text of the form field named "Header".
        FormEditor formEditor = new FormEditor(intermediatePath, outputPath);
        formEditor.Facade = new FormFieldFacade();
        formEditor.Facade.Alignment = FormFieldFacade.AlignCenter;
        formEditor.DecorateField("Header");
        formEditor.Save();

        // Remove the temporary file.
        try { File.Delete(intermediatePath); } catch { }

        Console.WriteLine($"Output saved to '{outputPath}'.");
    }
}