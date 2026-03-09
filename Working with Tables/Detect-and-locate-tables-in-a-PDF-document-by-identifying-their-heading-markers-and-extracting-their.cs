using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class TableDetection
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables on the document
            TableAbsorber absorber = new TableAbsorber();

            // Enable the flow engine to improve detection of tables without explicit borders
            absorber.UseFlowEngine = true;

            // Extract tables from the whole document
            absorber.Visit(doc);

            // If no tables were found, inform the user
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables detected in the document.");
                return;
            }

            // Iterate over each detected table
            for (int i = 0; i < absorber.TableList.Count; i++)
            {
                AbsorbedTable table = absorber.TableList[i];

                // The Rectangle property gives the position of the table on its page
                Aspose.Pdf.Rectangle rect = table.Rectangle;

                // Output page number and rectangle coordinates (lower‑left and upper‑right)
                Console.WriteLine($"Table {i + 1}:");
                Console.WriteLine($"  Page      : {table.PageNum}");
                Console.WriteLine($"  LLX, LLY  : {rect.LLX}, {rect.LLY}");
                Console.WriteLine($"  URX, URY  : {rect.URX}, {rect.URY}");

                // OPTIONAL: Identify a heading marker (e.g., the first text fragment above the table)
                // Here we look for the first text fragment on the same page whose bottom edge
                // is just above the table's lower edge (within a small tolerance).
                const double tolerance = 5.0; // points

                // Use a TextFragmentAbsorber to collect all text fragments on the page
                TextFragmentAbsorber textAbsorber = new TextFragmentAbsorber();
                doc.Pages[table.PageNum].Accept(textAbsorber);

                // Find the nearest fragment above the table
                TextFragment heading = null;
                double minDistance = double.MaxValue;

                foreach (TextFragment fragment in textAbsorber.TextFragments)
                {
                    // Fragment's rectangle
                    Aspose.Pdf.Rectangle fragRect = fragment.Rectangle;

                    // Check if fragment is horizontally overlapping the table
                    bool horizontalOverlap = !(fragRect.URX < rect.LLX || fragRect.LLX > rect.URX);

                    // Check if fragment is just above the table
                    if (horizontalOverlap && fragRect.URY <= rect.LLY && (rect.LLY - fragRect.URY) <= tolerance)
                    {
                        double distance = rect.LLY - fragRect.URY;
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            heading = fragment;
                        }
                    }
                }

                if (heading != null)
                {
                    Console.WriteLine($"  Heading   : \"{heading.Text}\"");
                }
                else
                {
                    Console.WriteLine("  Heading   : (none detected)");
                }
            }
        }
    }
}
