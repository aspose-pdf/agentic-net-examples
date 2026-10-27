using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Namespace may not contain XfdfWriter in all versions

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare a memory stream that will hold the XFDF data
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                try
                {
                    // Attempt to obtain the XfdfWriter type via reflection.
                    // This avoids compile‑time errors if the class is absent in the referenced Aspose.Pdf version.
                    Type writerType = Type.GetType("Aspose.Pdf.Facades.XfdfWriter, Aspose.Pdf");

                    if (writerType != null)
                    {
                        // Create an instance of XfdfWriter
                        object writerInstance = Activator.CreateInstance(writerType);

                        // Locate the Export method: Export(Document, int[] pages, Stream)
                        var exportMethod = writerType.GetMethod(
                            "Export",
                            new Type[] { typeof(Document), typeof(int[]), typeof(Stream) });

                        if (exportMethod != null)
                        {
                            // Export annotations from pages 1 and 2 into the memory stream
                            exportMethod.Invoke(writerInstance, new object[] { doc, new int[] { 1, 2 }, xfdfStream });

                            // Reset stream position for reading
                            xfdfStream.Position = 0;

                            // Example processing: read the XFDF content as a string
                            using (StreamReader reader = new StreamReader(xfdfStream))
                            {
                                string xfdfContent = reader.ReadToEnd();
                                Console.WriteLine("XFDF export succeeded. Content:");
                                Console.WriteLine(xfdfContent);
                            }
                        }
                        else
                        {
                            Console.WriteLine("XfdfWriter.Export method not found. Export step skipped.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("XfdfWriter class not present in this Aspose.Pdf version. Export step skipped.");
                    }
                }
                catch (Exception ex)
                {
                    // Preserve program flow even if export fails
                    Console.WriteLine($"Error during XFDF export: {ex.Message}");
                }
            }
        }
    }
}