using System;
using System.IO;
using Aspose.Pdf.Facades;

public class PdfFormProcessor
{
    public static byte[] FillPdfForm(byte[] pdfBytes)
    {
        // Load the PDF from the input byte array
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        // Initialize the Form facade with the PDF stream
        using (Form pdfForm = new Form(inputStream))
        {
            // Fill a text field (replace "Name" with the actual field name)
            pdfForm.FillField("Name", "John Doe");
            // Fill a checkbox field (replace "Subscribe" with the actual field name)
            pdfForm.FillField("Subscribe", true);

            // Save the modified PDF into an output memory stream
            using (MemoryStream outputStream = new MemoryStream())
            {
                pdfForm.Save(outputStream);
                // Return the updated PDF as a byte array
                return outputStream.ToArray();
            }
        }
    }
}

public class Program
{
    // Entry point required for a console application
    public static void Main(string[] args)
    {
        // Simple demonstration: expects input and output file paths as arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputPdfPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Read the source PDF into a byte array
        byte[] sourcePdf = File.ReadAllBytes(inputPath);

        // Fill the form fields
        byte[] updatedPdf = PdfFormProcessor.FillPdfForm(sourcePdf);

        // Write the updated PDF back to disk
        File.WriteAllBytes(outputPath, updatedPdf);

        Console.WriteLine($"Updated PDF saved to: {outputPath}");
    }
}
