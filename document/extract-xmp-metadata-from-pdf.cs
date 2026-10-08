using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmpPath = "metadata.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Use a using block for deterministic disposal of the Document.
            using (Document doc = new Document(pdfPath))
            {
                // The Metadata property returns a Metadata object that represents the native XMP packet.
                Metadata metadata = doc.Metadata;

                // If the document has no XMP metadata, the Metadata object will be empty (its string representation is empty).
                if (metadata == null || string.IsNullOrEmpty(metadata.ToString()))
                {
                    Console.WriteLine("No XMP metadata found in the PDF.");
                }
                else
                {
                    // Convert the Metadata object to its XML string representation and write it to a file.
                    string xmpXml = metadata.ToString();
                    File.WriteAllText(xmpPath, xmpXml);
                    Console.WriteLine($"XMP metadata extracted to '{xmpPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
