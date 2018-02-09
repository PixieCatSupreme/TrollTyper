chatHandle = "asdfA"
name = "Harzuu"

chatColor = Color.Black

replacements = 
{
	TT.CreateReplacement("why", "y?"),
	TT.CreateReplacement("who", "o?"),
	TT.CreateReplacement("where", "wer?")
}

function PreQuirk(text)
	return text
end

function PostQuirk(text)
	return text
end