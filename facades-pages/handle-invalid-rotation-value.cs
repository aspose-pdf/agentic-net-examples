using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";

        // Example parameters (could be obtained from user input)
        int pageNumber = 1;          // 1‑based page index
        int rotationDegrees = 45;    // Invalid rotation for demonstration

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document to obtain the page count safely
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Validate page number (Aspose.Pdf uses 1‑based indexing)
        if (pageNumber < 1 || pageNumber > pageCount)
        {
            Console.Error.WriteLine($"Invalid page number {pageNumber}. Document contains {pageCount} pages.");
            return;
        }

        // Map the integer rotation to the Rotation enum; only four values are allowed
        Rotation rotationEnum;
        bool rotationIsValid = true;
        switch (rotationDegrees)
        {
            case 0:
                rotationEnum = Rotation.None; // 0° rotation
                break;
            case 90:
                rotationEnum = Rotation.on90;
                break;
            case 180:
                rotationEnum = Rotation.on180;
                break;
            case 270:
                rotationEnum = Rotation.on270;
                break;
            default:
                rotationIsValid = false;
                rotationEnum = Rotation.None; // placeholder, will not be used
                break;
        }

        // If the rotation value is not one of the allowed angles, report and exit
        if (!rotationIsValid)
        {
            Console.Error.WriteLine($"Invalid rotation value {rotationDegrees}. Allowed values are 0, 90, 180, 270.");
            return;
        }

        // Perform the rotation using the Document API and handle possible exceptions
        try
        {
            Document pdf = new Document(inputPath);
            pdf.Pages[pageNumber].Rotate = rotationEnum; // Apply rotation to the selected page
            pdf.Save(outputPath);
            Console.WriteLine($"Page {pageNumber} rotated to {rotationDegrees}° and saved as '{outputPath}'.");
        }
        catch (ArgumentException ex)
        {
            // Handles cases where the API receives an out‑of‑range argument
            Console.Error.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch‑all for any unexpected runtime errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
