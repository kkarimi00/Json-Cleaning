using System.Text.Json.Nodes;

public class CleanJsonString
{
    string jsonString = "{\"name\":{\"first\":\"Robert\",\"middle\":\"\",\"last\":\"Smith\"},\"age\":25,\"DOB\":\"-\",\"hobbies\":[\"running\",\"coding\",\"-\"],\"education\":{\"highschool\":\"N/A\",\"college\":\"Yale\"}}";

    Console.WriteLine(jsonString);

    void CleanString(string jsonString)
    {
        if (!string.IsNullOrEmpty(jsonString))
        {
            JsonObject? array = JsonNode.Parse(jsonString)?.AsObject();
            foreach (var node in array!.ToList())
            {
                JsonNode? jsonNode = node.Value != null ? node.Value : null;
                Type? type = jsonNode != null ? jsonNode.GetType() : null;

                if (type!.Equals(typeof(JsonObject)))
                {
                    if (node.Value != null && node.Value.AsObject().Count > 0)
                    {
                        foreach (var item in node.Value.AsObject().ToList())
                        {
                            string key = item.Key;
                            if (item.Value != null)
                            {
                                string value = item.Value.AsValue().ToString();

                                if (CheckValue(value))
                                    jsonNode!.AsObject().Remove(key);
                            }
                        }
                    }
                }
                else if (type.Equals(typeof(JsonArray)))
                {
                    if (node.Value != null && node.Value.AsArray().Count > 0)
                    {
                        int j = node.Value.AsArray().Count;
                        for (var i = 0; i < j; i++)
                        {
                            var item = node.Value.AsArray()[i];
                            if (CheckJsonNodeValue(item))
                            {
                                jsonNode!.AsArray().Remove(item);
                                j--; i--;
                            }
                        }
                    }
                }
                else
                {
                    string key = node.Key;
                    if (node.Value != null)
                    {
                        string value = node.Value.AsValue().ToString();

                        if (CheckValue(value))
                            array!.Remove(key);
                    }
                }

            }
            Console.WriteLine(array);
        }
    }

    bool CheckValue(string value)
    {
        if (value == "-"
                || value == "N/A"
                || value == "")
            return true;
        return false;
    }

    bool CheckJsonNodeValue(JsonNode? value)
    {
        if (value != null &&
            (value.ToString() == "-"
                || value.ToString() == "N/A"
                || value.ToString() == ""))
            return true;
        return false;
    }
}
