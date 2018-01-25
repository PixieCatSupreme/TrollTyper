chatHandle = "channelingConstruction"
name = "Mark"

chatColor = Utilities.CreateColor(75, 200, 48)

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