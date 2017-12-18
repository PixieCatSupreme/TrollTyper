chatHandle = "testyBoi"
name = "Test"

chatColor = Utilities.CreateColor(0, 0, 0)

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