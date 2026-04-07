using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "graph.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document containing the graph
        using (Document doc = new Document(pdfPath))
        {
            // Serialize the PDF to a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the document into the stream (PDF format is implicit)
                doc.Save(ms);
                // Obtain the byte array for network transmission
                byte[] pdfBytes = ms.ToArray();

                // Example usage: display the size of the serialized data
                Console.WriteLine($"PDF serialized to byte array, length = {pdfBytes.Length}");
                // At this point, pdfBytes can be sent over a network socket, HTTP response, etc.
            }
        }
    }
}