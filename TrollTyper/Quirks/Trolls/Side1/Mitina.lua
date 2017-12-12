chatHandle = "fierceFeline"

chatColor = 
{
  R = 161,
  B = 0,
  G = 0
}

replacements = 
{
  ValueReplacement("W", "oWo", true),
  ValueReplacement("l", "w"),
  ValueReplacement("L", "W"),
  ValueReplacement("r", "w"),
  ValueReplacement("R", "W"),
  ValueReplacement("n", "ny"),
  ValueReplacement("N", "Ny"),
  ValueReplacement("you", "U", true)
}

function PreQuirk(text)
  return text
end

function PostQuirk(text)
  return text
end