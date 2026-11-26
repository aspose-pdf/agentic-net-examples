using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "DefaultZoom.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document pdf = new Document())
        {
            // Add a blank page (first page)
            pdf.Pages.Add();

            // Define a GoTo action that points to the first page with XYZ explicit destination (0,0) and zoom 1.0
            GoToAction openAction = new GoToAction
            {
                Destination = new XYZExplicitDestination(1, 0, 0, 1.0)
            };

            // Assign the action as the document's open action
            pdf.OpenAction = openAction;

            // Save the PDF using the default PDF format
            pdf.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}