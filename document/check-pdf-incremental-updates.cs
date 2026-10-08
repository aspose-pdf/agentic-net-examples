using System;
using System.IO;
using Aspose.Pdf;

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
            // Aspose.Pdf exposes CreationDate (DateTime) and ModDate (DateTime).
            // Compare them to infer whether the file has been modified after its initial creation.
            bool hasModification = false;
            if (doc.Info != null)
            {
                DateTime creation = doc.Info.CreationDate;
                DateTime modification = doc.Info.ModDate;

                // When a PDF has never been modified, ModDate may be DateTime.MinValue.
                if (modification != DateTime.MinValue && modification > creation)
                {
                    hasModification = true;
                }
            }

            if (hasModification)
            {
                Console.WriteLine("The PDF appears to have been modified after its initial creation.");
            }
            else
            {
                Console.WriteLine("No modification detected; the PDF appears unchanged since creation.");
            }

            // Optionally display the creation and modification dates if available
            if (doc.Info != null)
            {
                Console.WriteLine($"Creation date: {doc.Info.CreationDate}");
                // ModDate may be unset (MinValue); display only when meaningful.
                if (doc.Info.ModDate != DateTime.MinValue)
                {
                    Console.WriteLine($"Last modification date: {doc.Info.ModDate}");
                }
            }
        }
    }
}
