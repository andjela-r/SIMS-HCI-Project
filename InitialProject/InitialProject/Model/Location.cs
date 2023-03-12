using System;

public class Location
{
	public string City { get; set; }
	public string Country { get; set; };

	public Location(string city, string county)
	{
		City = city; 
		Country = county;
	}

    public Location() { }
}
