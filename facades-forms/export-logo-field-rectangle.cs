using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "logo_rect.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(pdfPath);

        // Locate the field named "Logo" and obtain its rectangle
        Field logoField = doc.Form?.Fields?.FirstOrDefault(f => f.Name == "Logo");
        if (logoField == null || logoField.Rect == null)
        {
            Console.Error.WriteLine("Field 'Logo' not found or has no rectangle.");
            return;
        }

        Rectangle rect = logoField.Rect;

        string rectInfo = $"Logo field rectangle:{Environment.NewLine}" +
                          $"LLX: {rect.LLX}{Environment.NewLine}" +
                          $"LLY: {rect.LLY}{Environment.NewLine}" +
                          $"URX: {rect.URX}{Environment.NewLine}" +
                          $"URY: {rect.URY}";

        File.WriteAllText(outputPath, rectInfo);

        Console.WriteLine($"Rectangle exported to '{outputPath}'.");
    }
}
