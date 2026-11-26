using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "ZoomedNoTitle.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page before setting the OpenAction
            if (doc.Pages.Count > 0)
            {
                // Create an explicit XYZ destination for page 1 with 100% zoom (1.0)
                var destination = new XYZExplicitDestination(doc.Pages[1], 0, 0, 1.0);
                // Assign the GoToAction that uses the destination to the document's OpenAction
                doc.OpenAction = new GoToAction(destination);
            }

            // Disable the display of the document title in the viewer UI
            doc.DisplayDocTitle = false;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}
