using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF document using the core Document API (no Facades).
        using (Document doc = new Document(inputPath))
        {
            // Pages collection is 1‑based.
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];
                Rotation rotation = page.Rotate; // Rotation enum: None, on90, on180, on270

                if (rotation != Rotation.None)
                {
                    // Convert enum to a numeric angle for display purposes.
                    int angle = rotation == Rotation.on90 ? 90 :
                                rotation == Rotation.on180 ? 180 :
                                rotation == Rotation.on270 ? 270 : 0;

                    Console.WriteLine($"Page {pageNumber} rotated {angle}°. Scanning for tables...");

                    // TableAbsorber works on a single page; we call Visit(page) to limit the scope.
                    TableAbsorber absorber = new TableAbsorber();
                    absorber.Visit(page);

                    Console.WriteLine($"  Tables found: {absorber.TableList.Count}");
                    for (int t = 0; t < absorber.TableList.Count; t++)
                    {
                        var table = absorber.TableList[t];
                        // Rectangle is expressed in the page's coordinate system.
                        Console.WriteLine($"    Table {t + 1}: Bounds = {table.Rectangle}");
                    }
                }
                else
                {
                    Console.WriteLine($"Page {pageNumber} has no rotation. Skipping.");
                }
            }
        }
    }
}
