using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Actions live in the Annotations namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";   // re‑save the PDF (optional)
        const string zoomInfoPath  = "ZoomInfo.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            double zoomFactor = 0.0; // default when not specified

            // OpenAction may be any Action; check for GoToAction
            if (pdfDoc.OpenAction is GoToAction goTo && goTo.Destination != null)
            {
                // The Destination property is of type IAppointment. Cast to a concrete
                // explicit‑destination type to read the Zoom value.
                if (goTo.Destination is XYZExplicitDestination xyzDest)
                {
                    zoomFactor = xyzDest.Zoom; // Zoom is a double (0 = default)
                }
                else if (goTo.Destination is FitExplicitDestination)
                {
                    // Fit destinations do not carry a zoom factor – keep default 0.0
                    zoomFactor = 0.0;
                }
                else if (goTo.Destination is FitHExplicitDestination)
                {
                    zoomFactor = 0.0;
                }
                else if (goTo.Destination is FitVExplicitDestination)
                {
                    zoomFactor = 0.0;
                }
                else if (goTo.Destination is FitRExplicitDestination)
                {
                    zoomFactor = 0.0;
                }
                // Add other explicit‑destination types as needed.
            }

            // Write the zoom factor to a text file
            string zoomText = $"Zoom factor: {zoomFactor}";
            File.WriteAllText(zoomInfoPath, zoomText);
            Console.WriteLine($"Zoom information written to '{zoomInfoPath}'.");

            // Optional: save the PDF (no changes made)
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
        }
    }
}
