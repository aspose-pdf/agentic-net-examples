using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "customized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdf = new Document(inputPath);

        // Attempt to set viewer preferences if the current Aspose.Pdf version supports it
        try
        {
            dynamic doc = pdf;
            doc.ViewerPreferences.CenterWindow = true;
            doc.ViewerPreferences.HideMenuBar = true;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not available in the referenced Aspose.Pdf version. Skipping this step.");
        }

        // Save the customized PDF
        pdf.Save(outputPath);

        Console.WriteLine($"Customized PDF saved to '{outputPath}'.");
    }
}
