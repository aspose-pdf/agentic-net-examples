using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load options for XML → PDF conversion (XmlLoadOptions lives in Aspose.Pdf namespace)
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Document is wrapped in a using block for deterministic disposal (lifecycle rule)
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Viewer preference: resize the viewer window to fit the first displayed page
            pdfDoc.FitWindow = true; // Fit‑page mode

            // Additional optional viewer preferences (examples)
            // pdfDoc.CenterWindow = true;          // Center the window on screen
            // pdfDoc.HideMenubar = true;           // Hide the menu bar
            // pdfDoc.PageMode = PageMode.UseNone;  // No side panels shown

            // Save the resulting PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created with viewer preferences: {pdfPath}");
    }
}