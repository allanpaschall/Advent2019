#!/usr/bin/perl

$cnt = getpasswdcnt(264360,746325);
#$cnt = getpasswdcnt(256788,256790); # faster test pattern
print "Answer A: $cnt";

############ subroutines below here, at the bottom of the program, where the belong ##########

sub getpasswdcnt {
  # pass me the start and end of a range
  # and I return a count of "good" passwords 
  my ($a,$b) = @_;
  foreach my $n ($a .. $b){
     $cnt += meetscriteria($n);
  }
  return $cnt;
}

sub exactly2 {
  # part 2 demands exactly 2 digits in the password
  # build an associatve array keyed on each digit, value = occurances
  # it's good if there is any with 2 occurances
  my @a = @_;
  my %cnt;
  foreach (@a) {
    $cnt{$_}++;
  }
  foreach (values %cnt) {
    return 1 if $_ == 2;
  }
  return 0
}

sub meetscriteria {
  my $a = shift;
  # split out password digits into array, test it for sequential non-downness
  # if that and exact and duplicate digit requirements are met, return 1
  # return 0 unless $a =~ /(\d)\1/; # part 1
  my @digs = split("",$a);
  return 0 unless exactly2(@digs); # part 2
  for (my $i = 0; $i < 5; $i++) {
    if ($digs[$i] > $digs[$i+1]) {
        return 0;
    }
  }
  return 1;
}
