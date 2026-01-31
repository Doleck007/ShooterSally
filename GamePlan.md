# Oil Man - Game Design Document

## High Concept
A top-down shooter for iOS where players build and defend oil operations while raiding other players' bases to steal oil and equipment. Think "Clash of Clans meets Landman" - asymmetric PvP with base building and real-time raiding mechanics.

## Theme & Setting
- **Inspiration:** Landman TV series (Billy Bob Thornton)
- **Aesthetic:** Dieselpunk/steampunk oil industry (early 1900s-1920s oil boom era with stylized modern elements)
- **Tone:** Gritty, Texas oil patch, corporate espionage, cartel dealings
- **Visual Style:** 3D models, top-down camera perspective

## Core Gameplay Loop
1. **Build Your Oil Operation**
   - Establish drill sites
   - Build storage facilities
   - Set up defenses (turrets, traps, walls)
   - Hire cartel guards for protection

2. **Raid Other Players**
   - Real-time top-down shooter controls
   - Break into rival operations
   - Fight AI guards and defenses
   - Steal oil reserves and equipment
   - Escape before getting caught/killed

3. **Defend Passively**
   - Automated defense when you're offline
   - Guards, turrets, and traps protect your base
   - Other players raid your base asynchronously

4. **Upgrade & Progress**
   - Spend stolen oil on better equipment
   - Unlock new gear and weapons
   - Customize character appearance
   - Increase drilling capacity
   - Upgrade defenses

## Player Mechanics
### Movement
- Walk
- Run (sprint)
- Touch controls optimized for iOS (virtual joystick)

### Combat
- Aim and shoot
- Multiple weapon types (pistols, rifles, shotguns, etc.)
- Take cover
- Health/damage system

### Interaction
- Steal oil from storage tanks
- Hack terminals/doors
- Pick up equipment/loot
- Interact with story elements (future feature)

## Character System
- **Current Model:** Male character with tactical gear, goggles, practical outfit
- **Customization:** Gear, clothing, weapons, cosmetics
- **Future:** Female character variant and additional body types

## Multiplayer Architecture
- **Type:** Asynchronous PvP (like Clash of Clans)
- **Base Storage:** Backend server stores all player bases
- **Raiding:** Attack offline players, their defenses run on AI
- **No Real-time PvP:** Reduces networking complexity, mobile-friendly

## Resource System
- **Oil:** Primary currency, stolen from raids or produced by drills
- **Equipment:** Weapons, gear, defense structures
- **Progression:** Oil = upgrades = better raids = more oil

## AI Systems
- **Guard AI:** Patrol routes, detection range, combat behavior
- **Cartel Mercenaries:** Hire NPCs to defend or raid for you (stretch goal)
- **Defense Automation:** Turrets, traps trigger on player presence

## Platform & Technical
- **Platform:** iOS (iPhone/iPad)
- **Engine:** Unity 3D
- **Controls:** Touch-optimized (virtual joystick + tap to shoot)
- **Performance:** Optimized for mobile (polygon count, texture sizes)
- **Backend:** Player base storage, matchmaking (TBD: PlayFab, Photon, or custom)

## Development Phases

### PHASE 1: Proof of Concept (4-8 weeks) - CURRENT
**Goal:** Build a playable vertical slice to validate core mechanics

**MVP Features:**
- ✅ Player movement (walk/run) with animation
- ✅ Camera following player
- ⬜ Shooting mechanics with animations
- ⬜ Basic enemy AI (guards that patrol and shoot back)
- ⬜ Health/damage system
- ⬜ ONE pre-built base to raid
- ⬜ Oil to steal + escape zone
- ⬜ Win/lose conditions
- ⬜ Runs on iOS device

**NOT in MVP:**
- Base building
- Multiplayer backend
- Character customization
- Multiple levels
- Quest/story system
- Fancy UI/menus

**Success Criteria:** Friends play it and say "this is fun, I want more"

### PHASE 2: Expanded Prototype (if Phase 1 succeeds)
- Add 3-5 more raid scenarios
- Basic base building (place defenses)
- Simple progression system
- Touch controls refinement
- Polish and iOS performance optimization

### PHASE 3: Multiplayer Backend (if Phase 2 succeeds)
- Backend infrastructure for storing player bases
- Matchmaking system
- Real asynchronous raiding
- Player progression saves
- Anti-cheat basics

### PHASE 4: Content & Polish
- Character customization
- Multiple maps/environments
- Story/quest system (optional)
- Launch screen, main menu, character selection
- Store icon and branding
- Monetization (cosmetics, speed-ups, premium guards)

## Monetization Strategy (Future)
- **Free to Play** with optional purchases
- **Cosmetics:** Character skins, weapon skins
- **Convenience:** Speed up oil drilling, instant upgrades
- **Premium Units:** Special cartel guards with unique abilities
- **Battle Pass:** Seasonal content and rewards

## Unique Selling Points
1. **Theme:** Oil industry setting is unique, taps into Landman popularity
2. **Asymmetric Gameplay:** Build AND raid, dual satisfaction
3. **Mobile-Friendly:** Asynchronous PvP works for burst play sessions
4. **Cartel Mechanics:** Hire mercenaries adds strategic layer
5. **Risk/Reward:** Raiding is high stakes, defenses matter

## Story/Lore (Stretch Goal)
- You're a fixer/landman in the Texas oil fields
- Corporate espionage between rival oil companies
- Cartel involvement in the oil trade
- Territorial disputes over drilling rights
- Character progression through story missions
- Unlock lore about the oil boom era

## Technical Considerations
- **iOS Requirements:** Touch controls, performance on older devices
- **App Store Approval:** Violence levels appropriate, no real gambling
- **Backend Costs:** Server hosting for player bases
- **Cheating Prevention:** Server-authoritative for loot/progression
- **Testing:** Multiple device sizes and iOS versions

## Open Questions
- Which backend service? (PlayFab, Photon, custom AWS?)
- Real-time raid replays for defenders to watch?
- Guild/clan system for cooperative play?
- PvE campaign mode in addition to PvP?
- How to balance raiding difficulty vs defense strength?

## Development Philosophy
- **Ruthless Scope Control:** Ship small, iterate based on feedback
- **Mobile-First:** Everything designed for touch and burst sessions
- **Prove Fun First:** Don't build infrastructure until core loop is validated
- **Learn Unity:** This is also an educational project
- **Bring in Help:** Show prototype to friends, recruit collaborators if it works

---

**Last Updated:** January 2026  
**Current Status:** Phase 1 - Proof of Concept (Player movement done, working on shooting)