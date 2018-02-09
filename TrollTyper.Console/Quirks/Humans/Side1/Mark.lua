chatHandle = "channelingConstruction"
name = "Mark"

chatColor = TT.CreateColor(75, 200, 48)

replacements = 
{
	TT.CreateReplacement("test", "TEST")
}

function PreQuirk(text)
	return text
end

function PostQuirk(text)
	return text
end