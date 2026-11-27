using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Extract text from page 22
            TextAbsorber absorber22 = new TextAbsorber();
            pdfDoc.Pages[22].Accept(absorber22);
            string page22Text = absorber22.Text;

            // Extract text from page 23
            TextAbsorber absorber23 = new TextAbsorber();
            pdfDoc.Pages[23].Accept(absorber23);
            string page23Text = absorber23.Text;

            // Combine the extracted texts
            string combinedText = page22Text + Environment.NewLine + page23Text;

            // Write the combined result to a text file
            File.WriteAllText(outputTxtPath, combinedText);
        }

        Console.WriteLine($"Extracted text from pages 22 and 23 saved to '{outputTxtPath}'.");
    }
}
