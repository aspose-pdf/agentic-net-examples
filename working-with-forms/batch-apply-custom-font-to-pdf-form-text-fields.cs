using System;
using System.IO;
using System.Drawing; // kept for potential other drawing needs, but Color will be fully qualified
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for DefaultAppearance
using Aspose.Pdf.Text;        // for FontRepository and Font

class BatchFontApplier
{
    static void Main()
    {
        // UNC or network folder containing the PDFs
        const string networkFolder = @"\\Server\Shared\PDFs";
        // Path to the custom TrueType/OpenType font file
        const string customFontPath = @"C:\Fonts\MyCustomFont.ttf";

        // Verify the font file exists
        if (!File.Exists(customFontPath))
        {
            Console.Error.WriteLine($"Custom font not found: {customFontPath}");
            return;
        }

        // Load the custom font once and reuse it for all documents
        Aspose.Pdf.Text.Font customFont = FontRepository.OpenFont(customFontPath);

        // Get all PDF files in the network folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(networkFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Open each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate over all form fields
                    foreach (Field field in doc.Form.Fields)
                    {
                        // Apply the custom font only to text box fields
                        if (field is TextBoxField textBox)
                        {
                            // Create a new DefaultAppearance with the custom font.
                            // Size 12 and black colour are used as defaults; adjust as needed.
                            textBox.DefaultAppearance = new DefaultAppearance(customFont, 12, System.Drawing.Color.Black);
                        }
                    }

                    // Overwrite the original PDF with the updated version
                    doc.Save(pdfPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch font application completed.");
    }
}
