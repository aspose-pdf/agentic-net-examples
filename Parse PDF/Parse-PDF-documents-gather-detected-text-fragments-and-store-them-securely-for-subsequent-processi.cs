using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // source PDF
        const string outputPdfPath = "extracted_text.pdf";     // encrypted PDF with extracted text
        const string userPassword = "UserPass123";             // password for opening
        const string ownerPassword = "OwnerPass123";           // password for permissions

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                // Extract all text fragments from the whole document
                TextFragmentAbsorber fragmentAbsorber = new TextFragmentAbsorber();
                sourceDoc.Pages.Accept(fragmentAbsorber);

                // Create a new PDF to store the extracted text securely
                using (Document outputDoc = new Document())
                {
                    // Add a blank page
                    Page page = outputDoc.Pages.Add();

                    // Combine all fragment texts into a single string (preserving line breaks)
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (TextFragment fragment in fragmentAbsorber.TextFragments)
                    {
                        sb.AppendLine(fragment.Text);
                    }

                    // Add the combined text to the page
                    TextFragment tf = new TextFragment(sb.ToString())
                    {
                        // Optional: set appearance (font size, color) using DefaultAppearance constructor
                        // DefaultAppearance uses System.Drawing.Color for the third argument
                        // Here we keep defaults; no need to set explicitly
                    };
                    page.Paragraphs.Add(tf);

                    // Encrypt the output PDF (AES-256 recommended)
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                    outputDoc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                    // Save the encrypted PDF
                    outputDoc.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"Extracted text saved securely to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}