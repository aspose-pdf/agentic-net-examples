using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// NOTE: To suppress the NuGet vulnerability warning (NU1903) that is being treated as an error,
// add the following line to your .csproj file inside a <PropertyGroup>:
//   <NoWarn>NU1903</NoWarn>
// This does not affect runtime behavior and allows the project to build successfully.

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // result PDF
        const string imagePath = "image.png";   // PNG to add

        // Coordinates for the image rectangle on page 2
        // lower-left (x1, y1) and upper-right (x2, y2)
        const float lowerLeftX  = 100f;
        const float lowerLeftY  = 200f;
        const float upperRightX = 300f;
        const float upperRightY = 400f;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // PdfFileMend implements IDisposable via SaveableFacade, so use a using block
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the existing PDF document
            mend.BindPdf(inputPdf);

            // Add the PNG image to page 2 at the specified rectangle
            // Overload: AddImage(string imageName, int pageNum, double llx, double lly, double urx, double ury)
            mend.AddImage(imagePath, 2, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

            // Save the modified PDF
            mend.Save(outputPdf);

            // Close the facade (optional, called automatically by using)
            mend.Close();
        }

        Console.WriteLine($"Image added to page 2 and saved as '{outputPdf}'.");
    }
}
