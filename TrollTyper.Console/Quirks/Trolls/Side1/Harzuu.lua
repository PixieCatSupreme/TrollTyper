chatHandle = "asdfA"
name = "Harzuu"

chatColor = Utilities.CreateColor(0, 0, 0)

replacements = 
{
	ValueReplacement("why", "y?"),
	ValueReplacement("who", "o?"),
	ValueReplacement("where", "wer?")
}

function PreQuirk(text)
	return text
end

function PostQuirk(text)
	return text
end