namespace PreDiplom;

public class inventory
{
    public int ID { get; set; }
    public string Equipment_id { get; set; }
    public string Place_id { get; set; }
    public int Count { get; set; }
}

public class equipment
{
    public int ID { get; set; }
    public string name { get; set; }
    public int model_id { get; set; }
}

public class place
{
    public int Id { get; set; }
    public string special { get; set; }
    public int Employee_id { get; set; }
}