chatHandle = "tightsuitTitan"
name = "Stan"

chatColor = Utilities.CreateColor(226, 176, 40)

replacements = 
{
	ValueReplacement("test", "TEST")
}

function PreQuirk(text)
	return text
end

function PostQuirk(text)
	return text
end